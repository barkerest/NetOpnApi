using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetOpnApiBuilder.Controllers
{
    [Route("controller")]
    public class ControllerController : Controller
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            return NotFound();
        }

        [HttpGet("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            return NotFound();
        }
        
        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            return NotFound();
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Update(int id)
        {
            return NotFound();
        }

        
    }
}
