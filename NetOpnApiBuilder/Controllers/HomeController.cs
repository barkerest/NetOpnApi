using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetOpnApiBuilder.Models;
using NetOpnApiBuilder.ViewModels;

namespace NetOpnApiBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly BuilderDb _db;

        public HomeController(BuilderDb db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
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

        [HttpGet]
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
    }
}
