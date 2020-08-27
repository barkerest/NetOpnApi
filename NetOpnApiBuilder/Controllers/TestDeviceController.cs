using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.Controllers
{
    public class TestDeviceController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public TestDeviceController(BuilderDb db, ILogger<TestDeviceController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<IActionResult> Index()
        {
            var device = await _db.GetTestDeviceAsync();
            return View(device);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string host, string key, string secret)
        {
            var device = await _db.GetTestDeviceAsync();
            device.Host   = host;
            device.Key    = key;
            device.Secret = secret;

            if (!TryValidateModel(device))
            {
                return View("Index", device);
            }
            
            _db.Update(device);
            try
            {
                await _db.SaveChangesAsync();
                this.AddFlashMessage("The test device information has been saved.");
                
                if (device.Test(_logger))
                {
                    this.AddFlashMessage("Connection test succeeded.", AlertType.Success);
                }
                else
                {
                    this.AddFlashMessage("Connection test failed.", AlertType.Warning);
                }

            }
            catch (DbUpdateException)
            {
                this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
            }

            
            return RedirectToAction("Index");
        }

        
    }
}
