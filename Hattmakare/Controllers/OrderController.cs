using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hattmakare.Controllers;

[Authorize]
[Route("order")]
public class OrderController : Controller 
{
[HttpGet("new/hats")]
public async Task<IActionResult> Hats()
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
