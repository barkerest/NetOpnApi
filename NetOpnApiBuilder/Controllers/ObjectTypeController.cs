using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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

            return RedirectToParent(model);
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
    }
}
