﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;

namespace NetOpnApiBuilder.Controllers
{
    [Route("module")]
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
            foreach (var item in model)
            {
                item.HasCommandChanges = await _db.ApiCommands.AnyAsync(x => (x.CommandChanged == true || x.NewCommand == true) && (x.Controller.ModuleID == item.ID));
            }
            return View(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            var model = await _db.ApiModules
                                 .Include(x => x.Source)
                                 .Include(x => x.Controllers)
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified module ID was invalid.", AlertType.Danger);
                return RedirectToAction("Index");
            }

            foreach (var item in model.Controllers)
            {
                item.HasCommandChanges = await _db.ApiCommands.AnyAsync(x => (x.CommandChanged == true || x.NewCommand == true) && x.ControllerID == item.ID);
            }

            return View(model);
        }

        [HttpGet("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            var model = await _db.ApiModules
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified module ID was invalid.", AlertType.Danger);
                return RedirectToAction("Index");
            }

            model.Skip = !model.Skip;
            _db.Update(model);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
            }

            return RedirectToAction("Index", null, fragment: $"mod{id}");
        }
        
        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ApiModules
                                 .Include(x => x.Source)
                                 .FirstOrDefaultAsync(x => x.ID == id);
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

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id, string clrName, bool skip)
        {
            var model = await _db.ApiModules
                                 .Include(x => x.Source)
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified module ID was invalid.", AlertType.Danger);
                return RedirectToAction("Index");
            }

            model.ClrName = clrName;
            model.Skip    = skip;
            
            if (!TryValidateModel(model))
            {
                return View("Edit", model);
            }

            _db.Update(model);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
                return View("Edit", model);
            }

            return RedirectToAction("Index", null, fragment: $"mod{id}");
        }

    }
}
