using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Models;
using NetOpnApiBuilder.ViewModels;

namespace NetOpnApiBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly BuilderDb      _db;
        private readonly ILoggerFactory _loggerFactory;

        public HomeController(BuilderDb db, ILoggerFactory loggerFactory)
        {
            _db            = db ?? throw new ArgumentNullException(nameof(db));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet("device-ready")]
        public async Task<IActionResult> DeviceReady()
        {
            var device = await _db.GetTestDeviceAsync();
            var ready  = await Task.Run(() => device.Test());
            var defs   = TestDevice.Default;
            return Json(
                new
                {
                    ready,
                    host       = device.Host,
                    configured = !string.Equals(device.Key, defs.Key) && !string.Equals(device.Secret, defs.Secret)
                }
            );
        }


        [HttpGet("export-status")]
        public async Task<IActionResult> ExportStatus()
        {
            var exporter = new ApiExporter(_db, _loggerFactory.CreateLogger<ApiExporter>());
            await exporter.InitAsync();

            var message = new StringBuilder();

            void AddMessage(int count, string singular, string plural)
                => message.AppendFormat(count == 1 ? singular : plural, count);

            AddMessage(
                exporter.Commands.Count,
                "There is 1 command marked for export.",
                "There are {0} commands marked for export."
            );

            if (exporter.CommandsMissingData.Any())
            {
                message.Append('\n');
                AddMessage(
                    exporter.CommandsMissingData.Count,
                    "There is 1 command that needs additional data configured in the database.",
                    "There are {0} commands that need additional data configured in the database."
                );
            }

            if (exporter.CommandsMissingTypes.Any())
            {
                message.Append('\n');
                AddMessage(
                    exporter.CommandsMissingTypes.Count,
                    "There is 1 command that needs type values selected for post and/or response content.",
                    "There are {0} commands that need type values selected for post and/or response content."
                );

            }

            if (exporter.CommandsWithInvalidTypes.Any())
            {
                message.Append('\n');
                AddMessage(
                    exporter.CommandsWithInvalidTypes.Count,
                    "There is 1 command that has invalid type values selected for post and/or response content.",
                    "There are {0} commands that have invalid type values selected for post and/or response content."
                );
            }

            if (exporter.Types.Any())
            {
                message.Append('\n');
                AddMessage(
                    exporter.Types.Count,
                    "There is 1 type marked for export.",
                    "There are {0} types marked for export."
                );
                if (exporter.TypesWithMissingTypes.Any())
                {
                    message.Append('\n');
                    AddMessage(
                        exporter.TypesWithMissingTypes.Count,
                        "There is 1 type that needs type values selected for one or more properties.",
                        "There are {0} types that need type values selected for one or more properties."
                    );
                }

                if (exporter.TypesWithInvalidTypes.Any())
                {
                    message.Append('\n');
                    AddMessage(
                        exporter.TypesWithInvalidTypes.Count,
                        "There is 1 type that has invalid type values selected for one or more properties.",
                        "There are {0} types that have invalid type values selected for one or more properties."
                    );
                }
            }

            if (exporter.Ready)
            {
                message.Append("\nReady to export.");
            }

            return Json(
                new
                {
                    totalCommands            = exporter.Commands.Count,
                    commandsMissingData      = exporter.CommandsMissingData.Count,
                    commandsMissingTypes     = exporter.CommandsMissingTypes.Count,
                    commandsWithInvalidTypes = exporter.CommandsWithInvalidTypes.Count,
                    totalTypes               = exporter.Types.Count,
                    typesMissingTypes        = exporter.TypesWithMissingTypes.Count,
                    typesWithInvalidTypes    = exporter.TypesWithInvalidTypes.Count,
                    ready                    = exporter.Ready,
                    message                  = message.ToString()
                }
            );
        }
    }
}
