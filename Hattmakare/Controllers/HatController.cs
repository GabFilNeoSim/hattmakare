using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Hats;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Hattmakare.Services;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Hattmakare.Controllers;

[Authorize]
[Route("hats")]
public class HatController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HatController> _logger;
    private readonly IImageService _imageService;

    public HatController(AppDbContext context, ILogger<HatController> logger, IImageService imageService)
    {
        _context = context;
        _logger = logger;
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var hats = await _context.Hats
              .Where(x => !x.IsDeleted && x.HatType.Name == "StandardHatt")
             .Select(x => new HatViewModel
             {
            Name = x.Name,
            Price = x.Price,
            Quantity = x.Quantity,
            Size = x.Size,
            Length = x.Length,
            Depth = x.Depth,
            Width = x.Width,
            ImageUrl = x.ImageUrl,
            Id = x.Id,
            
            
        }).ToListAsync();
       
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
    public async Task<IActionResult> AddHat([FromForm] AddHatViewModel newHat)
    {
        var hat = new Hat();
        hat.Name = newHat.Name;
        hat.Size = newHat.Size;
        hat.Length = newHat.Length ?? 0;
        hat.Depth = newHat.Depth ?? 0;
        hat.Width = newHat.Width ?? 0;
        hat.Quantity = newHat.Quantity;
        hat.Price = newHat.Price ?? 0;


        var image = await _imageService.UploadImageAsync(newHat.Image);
        hat.ImageUrl = image;

        await _context.Hats.AddAsync(hat);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");

        //return View(newHat);
    }

    [HttpGet("EditHat/{hid}")]
    public async Task<IActionResult> EditHat(int Hid)
    {
        var hat = await _context.Hats.FirstOrDefaultAsync(x => x.Id == Hid);

        var model = new EditHatViewModel
        {
            Name = hat.Name,
            Price = hat.Price,
            Size = hat.Size,
            Length = hat.Length,
            Depth = hat.Depth,
            Width = hat.Width,
            Quantity = hat.Quantity
        };

        return View(model);
    }

    [HttpPost("EditHat/{hid}")]
    public async Task<IActionResult> EditHat(EditHatViewModel selectedHat)
    {
        var hat = await _context.Hats.FirstOrDefaultAsync(x => x.Id == selectedHat.Hid);
        hat.Name = selectedHat.Name;
        hat.Price = selectedHat.Price;
        hat.Size = selectedHat.Size;
        hat.Length = selectedHat.Length;
        hat.Depth = selectedHat.Depth;
        hat.Width = selectedHat.Width;
        hat.Quantity = selectedHat.Quantity;

        if (selectedHat.Image != null)
        {
            var image = await _imageService.UploadImageAsync(selectedHat.Image);
            hat.ImageUrl = image;
        }

        _context.Hats.Update(hat);

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost("remove/{hid}")]
    public async Task<IActionResult> RemoveHat(int hid)
    {
        _logger.LogWarning("Failed to find: {a}", hid);
        var hat = await _context.Hats.FirstOrDefaultAsync(x => x.Id == hid);
        if (hat is null)
        {
            return View("asd");
        }
        hat.IsDeleted = true;

        if (!String.IsNullOrWhiteSpace(hat.ImageUrl)) 
        {
            _imageService.DeleteImage(hat.ImageUrl);
            hat.ImageUrl = null;
        }

        _context.Hats.Update(hat);
        
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
        //throw new NotImplementedException();
    }

    [HttpGet("SearchHat")]
    public IActionResult SearchHat(string searchTerm)
    {
        
        var allHats = _context.Hats.AsEnumerable();  

       
        allHats = allHats.Where(h => !h.IsDeleted);

       
        if (!string.IsNullOrEmpty(searchTerm))
        {
            allHats = allHats.Where(h => h.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        
        var model = allHats.Select(hat => new HatViewModel
        {
            Id = hat.Id,
            Name = hat.Name,
            ImageUrl = hat.ImageUrl,
            IsDeleted = hat.IsDeleted,
            Size = hat.Size,
            Quantity = hat.Quantity,
            Price = hat.Price

        }).ToList();

        return View("Index", model);  
    }

}
