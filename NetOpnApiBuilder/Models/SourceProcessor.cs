using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;

namespace NetOpnApiBuilder.Models
{
    public class SourceProcessor
    {
        // patter to locate each action function along with preceding comments, arguments, and function body.
        private static readonly Regex ActionLocator
            = new Regex(
                @"
(?:
    (?:^|\n)\s*
    (?<CMT>\/\*\*(?:(?:[^\*\/]|(?<!\*)\/)*\*)*\/)
\s*)?
(?:^|\n)
\s*public\s+function\s+(?<NAME>[a-zA-Z0-9_]+)Action\s*
\((?<ARGS>[^\)]*)\)\s*
(?<BODY>\{
    (?>
        \{(?<c>) |
        [^\{\}'""]+ |
        (?:'(?:[^'\\]|\\\S)*') |
        (?:""(?:[^""\\]|\\\S)*"") |
        \}(?<-c>)
    )*
    (?(c)(?!))
\})
",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline
            );

        private readonly ILogger                               _logger;
        private readonly BuilderDb                             _db;
        private readonly ApiSource                             _source;
        private readonly IReadOnlyDictionary<string, string[]> _apiModules;

        public bool ImplementsApi => _apiModules.Any();

        public SourceProcessor(ApiSource source, BuilderDb db, ILogger<SourceProcessor> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _db     = db ?? throw new ArgumentNullException(nameof(db));

            if (string.IsNullOrWhiteSpace(_source.TemporaryPath)) throw new ArgumentException("Temporary path cannot be blank.", nameof(source));

            var tmp = _source.TemporaryPath.TrimEnd('\\', '/') + "/src/opnsense/mvc/app/controllers/OPNsense";

            if (Directory.Exists(tmp))
            {
                _logger.LogDebug($"Source {_source} appears to include MVC code.");
                var mods = new Dictionary<string, string[]>();
                foreach (var mod in Directory.GetDirectories(tmp, "*", SearchOption.TopDirectoryOnly))
                {
                    var modName     = Path.GetFileName(mod);
                    var controllers = new List<string>();

                    var modApi = mod + "/Api";
                    if (Directory.Exists(modApi))
                    {
                        foreach (var controller in Directory.GetFiles(modApi, "*Controller.php", SearchOption.TopDirectoryOnly))
                        {
                            controllers.Add(controller);
                        }
                    }

                    if (controllers.Any())
                    {
                        _logger.LogInformation($"Found {controllers.Count} controllers in {modName} module under {_source}.");
                        mods.Add(modName, controllers.ToArray());
                    }
                }

                _apiModules = mods;
            }
            else
            {
                _apiModules = new Dictionary<string, string[]>();
            }
        }


        public void Process()
        {
            _logger.LogInformation($"Processing source {_source}...");
            foreach (var modName in _apiModules.Keys)
            {
                var mod = _db
                          .ApiModules
                          .FirstOrDefault(x => x.SourceID == _source.ID && x.ApiName == modName);

                if (mod is null)
                {
                    _logger.LogDebug($"> add module {modName}");
                    mod = new ApiModule()
                    {
                        SourceID = _source.ID,
                        ApiName  = modName,
                        ClrName  = modName.ToSafeClrName()
                    };
                    _db.Add(mod);
                    _db.SaveChanges();
                }

                mod.Source = _source;

                foreach (var ctlPath in _apiModules[modName])
                {
                    var ctlName = Path.GetFileNameWithoutExtension(ctlPath);
                    ctlName = ctlName.Substring(0, ctlName.Length - 10);

                    var ctl = _db
                              .ApiControllers
                              .FirstOrDefault(x => x.ModuleID == mod.ID && x.ApiName == ctlName);

                    if (ctl is null)
                    {
                        _logger.LogDebug($"> add controller {modName}:{ctlName}");
                        ctl = new ApiController()
                        {
                            ModuleID = mod.ID,
                            ApiName  = ctlName,
                            ClrName  = ctlName.ToSafeClrName()
                        };
                        _db.Add(ctl);
                        _db.SaveChanges();
                    }

                    var contents = File.ReadAllText(ctlPath);
                    contents = contents.Replace("\r\n", "\n");

                    foreach (Match match in ActionLocator.Matches(contents))
                    {
                        var cmdComment = match.Groups["CMT"].Value;
                        var cmdName    = match.Groups["NAME"].Value;
                        var cmdArgs    = match.Groups["ARGS"].Value;
                        var cmdBody    = match.Groups["BODY"].Value;

                        var cmdSig = $"function {cmdName}Action({cmdArgs})";

                        var cmd = _db
                                  .ApiCommands
                                  .FirstOrDefault(x => x.ControllerID == ctl.ID && x.ApiName == cmdName);

                        var sigChanged = !string.Equals(cmdSig, cmd?.Signature);

                        if (cmd is null)
                        {
                            _logger.LogDebug($"> added command {modName}:{ctlName}:{cmdName}");
                            cmd = new ApiCommand()
                            {
                                ControllerID = ctl.ID,
                                ApiName      = cmdName,
                                ClrName      = cmdName.ToSafeClrName(),
                                Comment      = cmdComment,
                                Body         = cmdBody,
                                Signature    = cmdSig,
                                // make a somewhat educated guess.
                                UsePost        = cmdBody.Contains("if ($this->request->isPost()"),
                                SourceVersion  = _source.Version,
                                NewCommand     = true, // flag as new
                                CommandChanged = false // but not changed
                            };
                            _db.Add(cmd);
                            _db.SaveChanges();
                        }
                        else
                        {
                            // flag a command if the signature, body, or doc comment changes.
                            var changed = !string.Equals(cmd.Comment, cmdComment) ||
                                          !string.Equals(cmd.Body, cmdBody) ||
                                          !string.Equals(cmd.Signature, cmdSig);

                            if (changed)
                            {
                                _logger.LogDebug($"> updated command {modName}:{ctlName}:{cmdName}");
                            }

                            if (changed || !string.Equals(cmd.SourceVersion, _source.Version))
                            {
                                cmd.Comment        = cmdComment;
                                cmd.Body           = cmdBody;
                                cmd.Signature      = cmdSig;
                                cmd.SourceVersion  = _source.Version;
                                cmd.CommandChanged = changed;
                                _db.Update(cmd);
                                _db.SaveChanges();
                            }
                        }

                        cmd.Controller = ctl;

                        // update URL params if the signature changed.
                        if (sigChanged)
                        {
                            var urlParams = cmdArgs
                                            .Split(',')
                                            .Select(x => x.Split('=', 2))
                                            .Select(x => new KeyValuePair<string, bool>(x[0].Trim(), x.Length > 1))
                                            .ToArray();

                            var existingUrlParams = _db.ApiUrlParams
                                                       .Where(x => x.CommandID == cmd.ID)
                                                       .OrderBy(x => x.Order)
                                                       .ToList();

                            // remove parameters that no longer exist.
                            foreach (var item in existingUrlParams
                                                 .Where(
                                                     x => urlParams
                                                         .All(y => y.Key != x.ApiName)
                                                 )
                                                 .ToArray())
                            {
                                _logger.LogDebug($"> removing url param {modName}:{ctlName}:{cmdName}:{item.ApiName}");
                                _db.Remove(item);
                                existingUrlParams.Remove(item);
                            }

                            // use fake order values to avoid UK constraint.
                            var order = 1000 + existingUrlParams.Count + urlParams.Length;
                            foreach (var item in existingUrlParams)
                            {
                                item.Order = order++;
                                _db.Update(item);
                            }

                            _db.SaveChanges();

                            // re-order and add parameters.
                            order = 1;
                            foreach (var param in urlParams)
                            {
                                var paramName = param.Key.TrimStart('$');
                                var nullable  = param.Value;
                                var item      = existingUrlParams.FirstOrDefault(x => x.ApiName == paramName);
                                if (item is null)
                                {
                                    _logger.LogDebug($"> adding url param {modName}:{ctlName}:{cmdName}:{paramName}");
                                    item = new ApiUrlParam()
                                    {
                                        CommandID = cmd.ID,
                                        ApiName   = paramName,
                                        ClrName   = paramName.ToSafeClrName(),
                                        AllowNull = nullable,
                                        DataType  = ApiDataType.String,
                                        Order     = order
                                    };
                                    _db.Add(item);
                                    existingUrlParams.Add(item);
                                }
                                else
                                {
                                    if (item.AllowNull != nullable)
                                    {
                                        _logger.LogDebug($"> updating url param {modName}:{ctlName}:{cmdName}:{paramName}");
                                    }

                                    item.Order     = order;
                                    item.AllowNull = nullable;
                                    _db.Update(item);
                                }

                                order++;
                            }

                            _db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
