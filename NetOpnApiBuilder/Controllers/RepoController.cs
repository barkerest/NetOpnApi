using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.Controllers
{
    public class RepoController : Controller
    {
        private readonly Repos          _repos;
        private readonly BuilderDb      _db;
        
        public RepoController(Repos repos, BuilderDb db)
        {
            _repos         = repos ?? throw new ArgumentNullException(nameof(repos));
            _db            = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpPost]
        public async Task<IActionResult> Sync()
        {
            try
            {
                await Task.Run(() => _repos.Synchronize(_db));
                return Json(new {result = "ok"});
            }
            catch (InvalidOperationException e)
            {
                return Json(new {result = "failed", message = e.Message});
            }
            catch (AggregateException e) when (e.InnerExceptions.FirstOrDefault() is InvalidOperationException ex)
            {
                return Json(new {result = "failed", message = ex.Message});
            }
        }

        [HttpGet]
        public IActionResult Status()
        {
            var lastSync = _db.ApiSources.Where(x => x.LastSync != null).Max(x => x.LastSync);
            return Json(new
            {
                cloneComplete = _repos.InitComplete,
                syncComplete = _repos.Synchronized,
                syncRunning = _repos.Synchronizing,
                lastSync = lastSync?.ToString("yyyy-MM-dd HH:mm") ?? "never"
            });
        }
        
    }
}
