using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NetOpnApiBuilder.Models
{
    public class Repos
    {
        private class CoreRepo : GitRepo
        {
            public CoreRepo(ILogger logger)
                : base("https://github.com/opnsense/core", logger)
            {
            }
        }

        private class PluginsRepo : GitRepo
        {
            public PluginsRepo(ILogger logger)
                : base("https://github.com/opnsense/plugins", logger)
            {
            }
        }

        private readonly object         _taskLock = new object();
        private readonly Task           _initTask;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Determine if the repos have been initialized.
        /// </summary>
        public bool InitComplete => _initTask.IsCompleted;

        /// <summary>
        /// Determine if the repo initialization has been started.
        /// </summary>
        public bool InitStarted => _initTask.Status != TaskStatus.Created;

        /// <summary>
        /// Get the core repo.
        /// </summary>
        public GitRepo Core { get; private set; }

        /// <summary>
        /// Get the plugins repo.
        /// </summary>
        public GitRepo Plugins { get; private set; }

        private bool _syncRunning  = false;
        private bool _syncComplete = false;
        
        public Repos(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            
            _initTask = new Task(
                () =>
                {
                    Core    = new CoreRepo(loggerFactory.CreateLogger<CoreRepo>());
                    Plugins = new PluginsRepo(loggerFactory.CreateLogger<PluginsRepo>());
                }
            );
        }

        private static string GetSourceVersion(string path, string pattern)
        {
            if (!File.Exists(path)) return "0.0";

            var rex = new Regex(pattern);
            
            using (var rdr = new StreamReader(path))
            {
                var line = rdr.ReadLine();
                while (!(line is null))
                {
                    var m = rex.Match(line);
                    if (m.Success)
                    {
                        return m.Groups[1].Value;
                    }
                    
                    line = rdr.ReadLine();
                }
            }

            return "0.0";
        }

        public Task Initialize()
        {
            lock (_taskLock)
            {
                if (InitComplete)
                {
                    throw new InvalidOperationException("Initialization has already completed.");
                }
                
                if (InitStarted)
                {
                    throw new InvalidOperationException("Initialization has already started.");
                }

                _initTask.Start();
                return _initTask;
            }
        }
        
        public void Synchronize(BuilderDb db)
        {
            // make sure only one sync task can be running at a time.
            lock (_taskLock)
            {
                if (!InitComplete)
                {
                    throw new InvalidOperationException("Sync cannot start until repos are initialized.");
                }
                
                if (Synchronizing)
                {
                    throw new InvalidOperationException("Sync is already running.");
                }

                _syncRunning = true;
            }

            try
            {
                var sourceLogger = _loggerFactory.CreateLogger<SourceProcessor>();

                // process core repo.
                var sourceName = Core.Name.Replace('\\', '/');
                var source     = db.ApiSources.FirstOrDefault(x => x.Name == sourceName);
                if (source is null)
                {
                    source = new ApiSource()
                    {
                        Name    = sourceName,
                        Version = "0.0"
                    };
                    db.Add(source);
                    db.SaveChanges();
                }

                source.TemporaryPath = Core.LocalPath;
                source.Version       = GetSourceVersion(Core.LocalPath + "/Makefile", @"^\s*CORE_ABI\?=\s*(\S+)\s*$");

                var processor = new SourceProcessor(source, db, sourceLogger);
                processor.Process();

                source.LastSync = DateTime.Now;
                db.Update(source);
                db.SaveChanges();

                // now process plugins.
                foreach (var group in Directory.GetDirectories(Plugins.LocalPath, "*", SearchOption.TopDirectoryOnly))
                {
                    var groupName = Path.GetFileName(group);
                    foreach (var plugin in Directory.GetDirectories(group, "*", SearchOption.TopDirectoryOnly))
                    {
                        var pluginName = Path.GetFileName(plugin);
                        sourceName = $"{Plugins.Name.Replace('\\','/')}/{groupName}/{pluginName}";
                        source = db.ApiSources.FirstOrDefault(x => x.Name == sourceName);
                        if (source is null)
                        {
                            source = new ApiSource()
                            {
                                Name    = sourceName,
                                Version = "0.0"
                            };
                            db.Add(source);
                            db.SaveChanges();
                        }

                        source.TemporaryPath = plugin;
                        source.Version       = GetSourceVersion(plugin + "/Makefile", @"^\s*PLUGIN_VERSION=\s*(\S+)\s*$");

                        processor = new SourceProcessor(source, db, sourceLogger);
                        if (processor.ImplementsApi)
                        {
                            processor.Process();
                        }

                        source.LastSync = DateTime.Now;
                        db.Update(source);
                        db.SaveChanges();
                    }
                }

                _syncComplete = true;
            }
            finally
            {
                _syncRunning = false;
            }
        }

        public bool Synchronizing => _syncRunning;

        public bool Synchronized => _syncComplete;
    }
}
