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
    [Route("query-parameter")]
    public class QueryParameterController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public QueryParameterController(BuilderDb db, ILogger<QueryParameterController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [NonAction]
        private IActionResult RedirectToParent(ApiQueryParam model)
        {
            if (model is null)
            {
                return RedirectToAction("Index", "Module");
            }

            return RedirectToAction("Edit", "Command", new {id = model.Command?.ID ?? model.CommandID}, $"qp{model.ID}");
        }

        [HttpGet("/command/{commandId}/new-query-parameter")]
        public async Task<IActionResult> New(int commandId)
        {
            var command = await _db.ApiCommands
                                   .Include(x => x.Controller)
                                   .ThenInclude(x => x.Module)
                                   .ThenInclude(x => x.Source)
                                   .FirstOrDefaultAsync(x => x.ID == commandId);

            if (command is null)
            {
                this.AddFlashMessage("The specified command ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }
            
            var model = new ApiQueryParam()
            {
                Command = command
            };

            return View(model);
        }

        [HttpPost("/command/{commandId}/new-query-parameter")]
        public async Task<IActionResult> Create(int commandId, string apiName, string clrName, ApiDataType? dataType, bool allowNull)
        {
            var command = await _db.ApiCommands
                                   .Include(x => x.Controller)
                                   .ThenInclude(x => x.Module)
                                   .ThenInclude(x => x.Source)
                                   .FirstOrDefaultAsync(x => x.ID == commandId);

            if (command is null)
            {
                this.AddFlashMessage("The specified command ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }
            
            var model = new ApiQueryParam()
            {
                Command = command,
                ApiName = apiName,
                ClrName = clrName,
                DataType = dataType ?? ApiDataType.String,
                AllowNull =  allowNull
            };

            if (!TryValidateModel(model))
            {
                return View("New", model);
            }

            _db.Add(model);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
                return View("New", model);
            }

            return RedirectToParent(model);
        }
        

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ApiQueryParams
                                 .Include(x => x.Command)
                                 .ThenInclude(x => x.Controller)
                                 .ThenInclude(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified query parameter ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id, string apiName, string clrName, ApiDataType? dataType, bool allowNull)
        {
            var model = await _db.ApiQueryParams
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

            model.ApiName   = apiName;
            model.ClrName   = clrName;
            model.DataType  = dataType ?? ApiDataType.String;
            model.AllowNull = allowNull;

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

        [HttpGet("{id}/remove")]
        public async Task<IActionResult> Remove(int id)
        {
            var model = await _db.ApiQueryParams
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

            _db.Remove(model);
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
        
    }
}
