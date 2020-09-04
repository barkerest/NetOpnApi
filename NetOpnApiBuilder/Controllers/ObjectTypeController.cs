using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;
using NetOpnApiBuilder.Models;
using NetOpnApiBuilder.ViewModels;

namespace NetOpnApiBuilder.Controllers
{
    [Route("object-type")]
    public class ObjectTypeController : Controller
    {
        private readonly ILogger   _logger;
        private readonly BuilderDb _db;

        public ObjectTypeController(BuilderDb db, ILogger<ObjectTypeController> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [NonAction]
        private IActionResult RedirectToParent(ApiObjectType model)
        {
            if (model is null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", null, fragment: $"obj{model.ID}");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _db.ApiObjectTypes.OrderBy(x => x.Name).ToArrayAsync();
            return View(model);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            var model = new ApiObjectType();
            return View(model);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create(string name)
        {
            var model = new ApiObjectType()
            {
                Name = name
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

            return RedirectToAction("Show", new {id = model.ID});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            var model = await _db.ApiObjectTypes
                                 .Include(x => x.Properties)
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified object type ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            model.Sample = model.GenerateSample(_db);

            return View(model);
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ApiObjectTypes
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified object type ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            return View(model);
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id, string name)
        {
            var model = await _db.ApiObjectTypes
                                 .FirstOrDefaultAsync(x => x.ID == id);
            if (model is null)
            {
                this.AddFlashMessage("The specified object type ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            model.Name = name;

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

            return RedirectToParent(null);
        }

        [HttpGet("{id}/remove")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _db.ApiObjectTypes
                                .FirstOrDefaultAsync(x => x.ID == id);

            if (item is null)
            {
                this.AddFlashMessage("The specified object type ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            var deps = await _db.ApiObjectTypeReferences.CountAsync(x => x.ObjectTypeID == id);

            if (deps == 0)
            {
                // no dependencies, free to remove.
                _db.Remove(item);

                try
                {
                    await _db.SaveChangesAsync();
                    this.AddFlashMessage($"Removed {item} from database.");
                    item = null;
                }
                catch (DbUpdateException)
                {
                    this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
                }

                return RedirectToParent(item);
            }

            var model = new RemoveObjectTypeModel()
            {
                ObjectType = item,
                OtherObjectTypes = await _db.ApiObjectTypes
                                            .Where(x => x.ID != id)
                                            .OrderBy(x => x.Name)
                                            .ToArrayAsync()
            };

            return View(model);
        }

        [HttpPost("{id}/remove")]
        public async Task<IActionResult> Delete(int id, int? replaceWithId)
        {
            var item = await _db.ApiObjectTypes
                                .FirstOrDefaultAsync(x => x.ID == id);

            if (item is null)
            {
                this.AddFlashMessage("The specified object type ID was invalid.", AlertType.Danger);
                return RedirectToParent(null);
            }

            if (replaceWithId is null)
            {
                this.AddFlashMessage("You must select a replacement type before this type can be removed.", AlertType.Warning);
                var model = new RemoveObjectTypeModel()
                {
                    ObjectType = item,
                    OtherObjectTypes = await _db.ApiObjectTypes
                                                .Where(x => x.ID != id)
                                                .OrderBy(x => x.Name)
                                                .ToArrayAsync()
                };

                return View("Remove", model);
            }

            var otherItem = await _db.ApiObjectTypes
                                     .FirstOrDefaultAsync(x => x.ID == replaceWithId);
            if (otherItem is null)
            {
                this.AddFlashMessage("The specified replacement object type ID was invalid.", AlertType.Danger);
                return RedirectToParent(item);
            }

            if (id == replaceWithId)
            {
                this.AddFlashMessage("Replacement must not be the object type being removed.", AlertType.Warning);

                var model = new RemoveObjectTypeModel()
                {
                    ObjectType = item,
                    OtherObjectTypes = await _db.ApiObjectTypes
                                                .Where(x => x.ID != id)
                                                .OrderBy(x => x.Name)
                                                .ToArrayAsync()
                };

                return View("Remove", model);
            }

            try
            {
                await _db.Database.ExecuteSqlInterpolatedAsync($@"UPDATE ""ApiObjectProperties"" SET ""DataTypeObjectTypeID""={replaceWithId} WHERE ""DataTypeObjectTypeID""={id} AND ""ObjectTypeID""<>{id}");
                await _db.Database.ExecuteSqlInterpolatedAsync($@"UPDATE ""ApiCommands"" SET ""ResponseBodyObjectTypeID""={replaceWithId} WHERE ""ResponseBodyObjectTypeID""={id}");
                await _db.Database.ExecuteSqlInterpolatedAsync($@"UPDATE ""ApiCommands"" SET ""PostBodyObjectTypeID""={replaceWithId} WHERE ""PostBodyObjectTypeID""={id}");
                await _db.Database.ExecuteSqlInterpolatedAsync($@"DELETE FROM ""ApiObjectProperties"" WHERE ""ObjectTypeID""={id}");
                await _db.Database.ExecuteSqlInterpolatedAsync($@"DELETE FROM ""ApiObjectTypes"" WHERE ""ID""={id}");
                this.AddFlashMessage($"Removed {item} from database.");
            }
            catch (DbUpdateException)
            {
                this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
                return RedirectToParent(item);
            }
            catch (SqliteException)
            {
                this.AddFlashMessage("Failed to update the database.", AlertType.Danger);
                return RedirectToParent(item);
            }

            return RedirectToParent(null);
        }

        private async Task<int> GenerateFromJson(ImportFromJsonModel model)
        {
            JsonElement json;

            try
            {
                json = JsonDocument.Parse(model.Json).RootElement;
            }
            catch (JsonException e)
            {
                throw new ApplicationException("JSON text is invalid.\n" + e.Message);
            }
            
            if (json.ValueKind != JsonValueKind.Object &&
                json.ValueKind != JsonValueKind.Array)
            {
                throw new ApplicationException("Object types can only be imported from JSON objects or JSON object arrays.");
            }

            Dictionary<string, JsonElement> properties;
            if (json.ValueKind == JsonValueKind.Array || model.IsDictionary)
            {
                var children = json.ValueKind == JsonValueKind.Array
                                   ? json.EnumerateArray().ToArray()
                                   : json.EnumerateObject().Select(x => x.Value).ToArray();
                
                if (children.Length == 0)
                {
                    throw new ApplicationException("Object types cannot be imported from an empty array.");
                }

                if (children.Any(x => x.ValueKind != JsonValueKind.Object))
                {
                    throw new ApplicationException("Object types cannot be imported from arrays that contain anything other than objects.");
                }

                var allProps = children.SelectMany(x => x.EnumerateObject().Select(y => y.Name)).Distinct().ToArray();

                properties = allProps
                             .Select(
                                 x => new KeyValuePair<string, JsonElement>(
                                     x,
                                     children.First(y => y.TryGetProperty(x, out _)).GetProperty(x)
                                 )
                             )
                             .ToDictionary(x => x.Key, x => x.Value);
            }
            else
            {
                properties = json.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
            }

            ApiObjectType self;
            if (model.ObjectTypeId.HasValue)
            {
                self = await _db.ApiObjectTypes
                                .Include(x => x.Properties)
                                .ThenInclude(x => x.DataTypeObjectType)
                                .FirstOrDefaultAsync(x => x.ID == model.ObjectTypeId);

                if (self is null)
                {
                    throw new ApplicationException("The provided object type ID is invalid.");
                }

                _logger.LogDebug($"Updating existing type {self}.");
                if (!model.TypeName.StartsWith("?"))
                {
                    var cmdRefCnt = await _db.ApiCommands.CountAsync(x => x.PostBodyObjectTypeID == self.ID)
                                    + await _db.ApiCommands.CountAsync(x => x.ResponseBodyObjectTypeID == self.ID);
                    var propRefCnt = await _db.ApiObjectProperties.CountAsync(x => x.DataTypeObjectTypeID == self.ID);

                    if (cmdRefCnt <= 1 &&
                        propRefCnt <= 1 &&
                        !string.Equals(self.Name, model.TypeName))
                    {
                        _logger.LogDebug($" > changing type name to {model.TypeName}");
                        self.Name = model.TypeName;
                    }
                }

                self.ImportSample = JsonSerializer.Serialize(json, new JsonSerializerOptions() {WriteIndented = true});

                _db.Update(self);
                await _db.SaveChangesAsync();
            }
            else
            {
                self = new ApiObjectType()
                {
                    Name         = model.TypeName.TrimStart('?'),
                    Properties   = new List<ApiObjectProperty>(),
                    ImportSample = JsonSerializer.Serialize(json, new JsonSerializerOptions() {WriteIndented = true})
                };
                _logger.LogDebug($"Creating new type {self.Name}.");
                _db.Add(self);
                await _db.SaveChangesAsync();
            }

            foreach (var (apiName, jsonValue) in properties)
            {
                var prop = self.Properties.FirstOrDefault(x => string.Equals(apiName, x.ApiName, StringComparison.OrdinalIgnoreCase));
                if (prop is null)
                {
                    _logger.LogDebug($" > creating property: {apiName}");
                    prop = new ApiObjectProperty()
                    {
                        ObjectTypeID = self.ID,
                        ApiName      = apiName,
                        ClrName      = apiName.ToSafeClrName(),
                        ImportSample = JsonSerializer.Serialize(jsonValue, new JsonSerializerOptions() {WriteIndented = true})
                    };
                }
                else
                {
                    if (!string.Equals(apiName, prop.ApiName, StringComparison.Ordinal))
                    {
                        _logger.LogDebug($" > updating property name: {apiName}");
                        prop.ApiName = apiName;
                    }

                    prop.ImportSample = JsonSerializer.Serialize(jsonValue, new JsonSerializerOptions() {WriteIndented = true});
                }

                if (prop.ID == default)
                {
                    _db.Add(prop);
                }
                else
                {
                    _db.Update(prop);
                }
            }

            await _db.SaveChangesAsync();

            return self.ID;
        }

        [HttpPost("from-json")]
        public async Task<IActionResult> FromJson(ImportFromJsonModel model)
        {
            try
            {
                var newId = await GenerateFromJson(model);
                return RedirectToAction("Show", new {id = newId});
            }
            catch (ApplicationException e)
            {
                this.AddFlashMessage(e.Message, AlertType.Danger);
                return RedirectToAction("Index");
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportFromJson([FromBody] ImportFromJsonModel model)
        {
            try
            {
                var newId = await GenerateFromJson(model);
                var type  = await _db.ApiObjectTypes.FirstOrDefaultAsync(x => x.ID == newId);
                return Json(
                    new
                    {
                        status       = "ok",
                        objectTypeId = newId,
                        isNew        = !model.ObjectTypeId.HasValue,
                        typeName     = type.Name
                    }
                );
            }
            catch (ApplicationException e)
            {
                return Json(
                    new
                    {
                        status = "failed",
                        error  = e.Message
                    }
                );
            }
        }
    }
}
