using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Models;
using NetOpnApiBuilder.ViewModels;
using NetOpnApiBuilder.ViewModels.Home;

namespace NetOpnApiBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BuilderDb               _db;
        private readonly Repos                   _repos;
        
        public HomeController(
            Repos repos,    
            BuilderDb db,
                            ILogger<HomeController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _repos  = repos ?? throw new ArgumentNullException(nameof(db));
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel(_repos, _db));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet]
        public IActionResult ReposReady()
        {
            return Json(new {ready = _repos.InitComplete});
        }

        [HttpGet]
        public async Task<IActionResult> DeviceReady()
        {
            var device = await _db.GetTestDeviceAsync();
            var ready  = await Task.Run(() => device.Test());
            return Json(new {ready, host = device.Host});
        }
    }
}
