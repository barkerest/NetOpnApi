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
    [Route("controller")]
    public class ControllerController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public ControllerController(BuilderDb db, ILogger<ControllerController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [NonAction]
        private IActionResult RedirectToParent(ApiController ctlr)
        {
            if (ctlr is null)
            {
                return RedirectToAction("Index", "Module");
            }
            
            return RedirectToAction("Show", "Module", new {id = ctlr.ModuleID}, $"ctlr{ctlr.ID}");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            var model = await _db.ApiControllers
                           .Include(x => x.Module)
                           .ThenInclude(x => x.Source)
                           .Include(x => x.Commands)
                           .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified controller ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpGet("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            var model = await _db.ApiControllers
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified controller ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
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

            return RedirectToParent(model);
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ApiControllers
                                 .Include(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .Include(x => x.Commands)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified controller ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id, string clrName, bool skip)
        {
            var model = await _db.ApiControllers
                                 .Include(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .Include(x => x.Commands)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified controller ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
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

            return RedirectToParent(model);
        }
    }
}
