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
    [Route("object-property")]
    public class ObjectPropertyController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public ObjectPropertyController(BuilderDb db, ILogger<ObjectPropertyController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [NonAction]
        private IActionResult RedirectToParent(ApiObjectProperty model)
        {
            if (model is null)
            {
                return RedirectToAction("Index", "ObjectType");
            }

            var id = model.ObjectType?.ID ?? model.ObjectTypeID;
            return RedirectToAction("Show", "ObjectType", new {id = id}, $"prop{model.ID}");
        }
        

        [HttpGet("/object-type/{objectTypeId}/new-property")]
        public async Task<IActionResult> New(int objectTypeId)
        {
            var parent = await _db.ApiObjectTypes.FirstOrDefaultAsync(x => x.ID == objectTypeId);

            if (parent is null)
            {
                this.AddFlashMessage("The specified object type ID was invalid.", AlertType.Danger);
                return RedirectToAction("Index", "ObjectType");
            }
            
            var model = new ApiObjectProperty(){ObjectType = parent};

            return View(model);
        }

        [HttpPost("/object-type/{objectTypeId}/new-property")]
        public async Task<IActionResult> Create(int objectTypeId, string apiName, string clrName, ApiDataType? dataType, int? dataTypeObjectTypeId, bool canBeNull)
        {
            var parent = await _db.ApiObjectTypes.FirstOrDefaultAsync(x => x.ID == objectTypeId);

            if (parent is null)
            {
                this.AddFlashMessage("The specified object type ID was invalid.", AlertType.Danger);
                return RedirectToAction("Index", "ObjectType");
            }
            
            var model = new ApiObjectProperty()
            {
                ObjectType           = parent,
                ApiName              = apiName,
                ClrName              = clrName,
                DataType             = dataType ?? ApiDataType.String,
                CanBeNull            = canBeNull,
                DataTypeObjectTypeID = dataTypeObjectTypeId
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
            var model = await _db.ApiObjectProperties
                                 .Include(x => x.ObjectType)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified property ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id, string apiName, string clrName, ApiDataType? dataType, int? dataTypeObjectTypeId, bool canBeNull)
        {
            var model = await _db.ApiObjectProperties
                                 .Include(x => x.ObjectType)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified property ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            model.ApiName              = apiName;
            model.ClrName              = clrName;
            model.DataType             = dataType ?? ApiDataType.String;
            model.DataTypeObjectTypeID = dataTypeObjectTypeId;
            model.CanBeNull            = canBeNull;

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

        [HttpGet("{id}/set-type/{dataType}")]
        public async Task<IActionResult> SetType(int id, ApiDataType dataType)
        {
            var model = await _db.ApiObjectProperties
                                 .Include(x => x.ObjectType)
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified property ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            model.DataType = dataType;
            
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
            var model = await _db.ApiObjectProperties
                                 .FirstOrDefaultAsync(x => x.ID == id);

            if (model is null)
            {
                this.AddFlashMessage("The specified property ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            _db.Remove(model);
            try
            {
                await _db.SaveChangesAsync();
                this.AddFlashMessage($"Property {model} has been removed.", AlertType.Success);
            }
            catch (DbUpdateException)
            {
                this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
            }

            return RedirectToParent(model);
        }
    }
}
