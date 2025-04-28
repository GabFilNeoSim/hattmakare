using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Hats;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Hattmakare.Services;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hattmakare.Controllers;

[Authorize]
[Route("hats")]
public class HatController : Controller
{
    private readonly AppDbContext _context;
    private readonly IImageService _imageService;

    public HatController(AppDbContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new HatIndexViewModel
        {
            StandardHats = await _context.Hats
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
                 Id = x.Id
             }).ToListAsync(),

            SpecialHats = await _context.Hats
             .Where(x => !x.IsDeleted && x.HatType.Name == "Specialhatt")
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
                 Id = x.Id
             }).ToListAsync()
        };

        return View(model);
    }

    [HttpGet("add")]
    public async Task<IActionResult> Addhat()
    {
        var materials = await _context.Materials.ToListAsync();

        var viewModel = new AddHatViewModel
        {
            AvailableMaterials = materials.Select(m => new MaterialQuantityViewModel
            {
                MaterialId = m.Id,
                Name = m.Name,
                Unit = m.Unit,
                Price = m.Price
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddHat([FromForm] AddHatViewModel newHat)
    {
        int hatTypeId = (int)HatTypes.StandardHat;
        if (newHat.IsSpecial)
        {
            hatTypeId = (int)HatTypes.SpecialHat;
        }

        var hat = new Hat
        {
            Name = newHat.Name,
            Size = newHat.Size,
            Length = newHat.Length,
            Depth = newHat.Depth,
            Width = newHat.Width,
            Quantity = newHat.Quantity,
            Price = newHat.Price,
            HatMaterials = new List<HatMaterial>(),
            HatTypeId = hatTypeId,
        };
        

        var image = await _imageService.UploadImageAsync(newHat.Image);
        hat.ImageUrl = image;

        foreach (var material in newHat.SelectedMaterials)
        {
            if (material.Quantity > 0)
            {
                hat.HatMaterials.Add(new HatMaterial
                {
                    MaterialId = material.MaterialId,
                    Quantity = material.Quantity
                });
            }
        }

        await _context.Hats.AddAsync(hat);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    [HttpGet("{hid}/edit")]
    public async Task<IActionResult> EditHat(int Hid)
    {
        var hat = await _context.Hats
                .Include(h => h.HatMaterials)
                .ThenInclude(hm => hm.Material)
                .FirstOrDefaultAsync(h => h.Id == Hid);

        if (hat == null)
            return NotFound();

        var selectedMaterials = hat.HatMaterials.Select(hm => new MaterialQuantityViewModel
        {
            MaterialId = hm.MaterialId,
            Name = hm.Material.Name,
            Quantity = hm.Quantity,
            Unit = hm.Material.Unit,
            Price = hm.Material.Price
        }).ToList();

        var allMaterials = await _context.Materials.Select(m => new MaterialQuantityViewModel
        {
            MaterialId = m.Id,
            Name = m.Name,
            Unit = m.Unit,
            Price = m.Price
        }).ToListAsync();

        bool isSpecial = false;
        if (hat.HatTypeId == (int)HatTypes.SpecialHat)
        {
            isSpecial = true;
        }

        var model = new EditHatViewModel
        {
            Name = hat.Name,
            Price = hat.Price,
            Size = (int)hat.Size,
            Length = hat.Length,
            Depth = hat.Depth,
            Width = hat.Width,
            Quantity = (int)hat.Quantity,
            IsSpecial = isSpecial,
            SelectedMaterials = selectedMaterials,
            AvailableMaterials = allMaterials
        };

        return View(model);
    }

    [HttpPost("{hid}/edit")]
    public async Task<IActionResult> EditHat(EditHatViewModel selectedHat)
    {
        var hat = await _context.Hats
        .Include(h => h.HatMaterials)
        .FirstOrDefaultAsync(h => h.Id == selectedHat.Hid);

        if (hat == null)
        {
            return NotFound();
        }

        hat.Name = selectedHat.Name;
        hat.Price = selectedHat.Price;
        hat.Size = selectedHat.Size;
        hat.Length = selectedHat.Length;
        hat.Depth = selectedHat.Depth;
        hat.Width = selectedHat.Width;
        hat.Quantity = selectedHat.Quantity;

        int hatTypeId = (int)HatTypes.StandardHat;
        if (selectedHat.IsSpecial)
        {
            hatTypeId = (int)HatTypes.SpecialHat;
        }
        hat.HatTypeId = hatTypeId;

        if (selectedHat.Image != null)
        {
            var image = await _imageService.UploadImageAsync(selectedHat.Image);
            hat.ImageUrl = image;
        }

        hat.HatMaterials.Clear();

        foreach (var item in selectedHat.SelectedMaterials)
        {
            hat.HatMaterials.Add(new HatMaterial
            {
                MaterialId = item.MaterialId,
                Quantity = item.Quantity
            });
        }

        _context.Hats.Update(hat);

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost("{hid}/remove")]
    public async Task<IActionResult> RemoveHat(int hid)
    {
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

  [HttpGet("details")]
  public async Task<IActionResult> getHatDetails(int id)
  {
    var hat = await _context.Hats
        .FirstOrDefaultAsync(h => h.Id == id);
    if (hat == null)
    {
      return NotFound();
    }
    var model = new HatViewModel
    {
      Id = hat.Id,
      Name = hat.Name,
      Price = hat.Price,
      Size = hat.Size,
      Length = hat.Length,
      Depth = hat.Depth,
      Width = hat.Width,
      ImageUrl = hat.ImageUrl,
      Comment = hat.Comment,
      HatTypeId = hat.HatTypeId
    };
    return Ok(model);
  }

}
