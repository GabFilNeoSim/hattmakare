using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Hat;
using Hattmakare.Data;

namespace Hattmakare.Controllers;

[Route("hats")]
public class HatController : Controller
{
    private readonly AppDbContext _context;

    public HatController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
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

    [HttpPost("add")]
    public async Task<IActionResult> AddHat(AddHatViewModel newHat)
    {
        throw new NotImplementedException();
    }

    [HttpPost("remove/{hatId:int}")]
    public async Task<IActionResult> RemoveHat(int hatId)
    {
        throw new NotImplementedException();
    }
}
