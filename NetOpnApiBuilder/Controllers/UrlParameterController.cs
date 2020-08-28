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
    [Route("url-parameter")]
    public class UrlParameterController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public UrlParameterController(BuilderDb db, ILogger<UrlParameterController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [NonAction]
        private IActionResult RedirectToParent(ApiUrlParam model)
        {
            if (model is null)
            {
                return RedirectToAction("Index", "Module");
            }

            return RedirectToAction("Edit", "Command", new {id = model.Command?.ID ?? model.CommandID}, $"up{model.ID}");
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ApiUrlParams
                                 .Include(x => x.Command)
                                 .ThenInclude(x => x.Controller)
                                 .ThenInclude(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified URL parameter ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id, string clrName, ApiDataType? dataType)
        {
            var model = await _db.ApiUrlParams
                                 .Include(x => x.Command)
                                 .ThenInclude(x => x.Controller)
                                 .ThenInclude(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified URL parameter ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            model.ClrName  = clrName;
            model.DataType = dataType ?? ApiDataType.String;

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
