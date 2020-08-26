﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.Controllers
{
    [Route("command")]
    public class CommandController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public CommandController(BuilderDb db, ILogger<CommandController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [NonAction]
        private IActionResult RedirectToParent(ApiCommand cmd)
        {
            if (cmd is null)
            {
                return RedirectToAction("Index", "Module");
            }
            
            return RedirectToAction("Show", "Controller", new {id = cmd.ControllerID}, $"cmd{cmd.ID}");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            var model = await _db.ApiCommands
                                 .Include(x => x.Controller)
                                 .ThenInclude(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .Include(x => x.PostBodyObjectType)
                                 .Include(x => x.ResponseBodyObjectType)
                                 .Include(x => x.QueryParams)
                                 .Include(x => x.UrlParams)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified command ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpGet("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            var model = await _db.ApiCommands
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified command ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            model.Skip = !model.Skip;

            _db.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToParent(model);
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ApiCommands
                                 .Include(x => x.Controller)
                                 .ThenInclude(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified command ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id, string clrName, bool skip, bool usePost, int? postBodyObjectTypeId, int? responseBodyObjectTypeId)
        {
            var model = await _db.ApiCommands
                                 .Include(x => x.Controller)
                                 .ThenInclude(x => x.Module)
                                 .ThenInclude(x => x.Source)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified command ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            model.ClrName                  = clrName;
            model.Skip                     = skip;
            model.UsePost                  = usePost;
            model.ResponseBodyObjectTypeID = responseBodyObjectTypeId;
            model.PostBodyObjectTypeID     = postBodyObjectTypeId;

            if (!TryValidateModel(model))
            {
                return View("Edit", model);
            }

            _db.Update(model);
            await _db.SaveChangesAsync();

            return RedirectToParent(model);
        }
    }
}
