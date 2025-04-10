using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Hat;

namespace Hattmakare.Controllers
{
    [Route("hat")]
    public class HatController : Controller
    {
        [HttpGet("all")]
        public async Task<IActionResult> Index()
        {
            var hats = new HatsViewModel();
            hats.Hats = new List<HatViewModel>
            {
                new HatViewModel { Name = "Hat 1", ImageUrl = "500x500_placeholder.png" },
                new HatViewModel { Name = "Hat 2", ImageUrl = "500x500_placeholder.png" },
                new HatViewModel { Name = "Hat 3", ImageUrl = "500x500_placeholder.png" },
                new HatViewModel { Name = "Hat 4", ImageUrl = "500x500_placeholder.png" },
                new HatViewModel { Name = "Hat 5", ImageUrl = "500x500_placeholder.png" },
                new HatViewModel { Name = "Hat 6", ImageUrl = "500x500_placeholder.png" }
            };
            return View(hats);
        }
    }
}
