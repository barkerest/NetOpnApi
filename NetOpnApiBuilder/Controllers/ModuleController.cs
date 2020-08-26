using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public ModuleController(BuilderDb db, ILogger<ModuleController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            var model = await _db.ApiModules.Include(x => x.Source).ToArrayAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ApiModules.Include(x => x.Source).FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified module ID was invalid.", AlertType.Danger);
                return RedirectToAction("Index");
            }

            if (string.IsNullOrEmpty(model.ClrName))
            {
                model.ClrName = model.ApiName.ToSafeClrName();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string clrName)
        {
            var model = await _db.ApiModules.Include(x => x.Source).FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified module ID was invalid.", AlertType.Danger);
                return RedirectToAction("Index");
            }

            model.ClrName = clrName;
            
            if (!TryValidateModel(model))
            {
                return View("Edit", model);
            }

            _db.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
