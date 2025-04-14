using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Hats;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Microsoft.AspNetCore.Authorization;

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


    //[Authorize]
    [HttpGet("AddHat")]
    public IActionResult Addhat()
    {
        var hat = new AddHatViewModel();
        return View(hat);
    }

    //[Authorize]
    [HttpPost("AddHat")]
    public async Task<IActionResult> AddHat(AddHatViewModel newHat)
    {
        var hat = new Hat();
        hat.Name = newHat.Name;

        await _context.Hats.AddAsync(hat);
        await _context.SaveChangesAsync();

        return View(newHat);
    }

    [HttpPost("remove/{hatId:int}")]
    public async Task<IActionResult> RemoveHat(int hatId)
    {
        Data.Entities.Hat hat = _context.Hats.Find(hatId);
        _context.Hats.Remove(hat); //Ändra denna till true or false

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
        //throw new NotImplementedException();
    }
}
