using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hattmakare.Data;
using Hattmakare.Models.Material;
using Microsoft.EntityFrameworkCore;
using Hattmakare.Data.Entities;

namespace Hattmakare.Controllers;

[Authorize]
[Route("materials")]
public class MaterialController : Controller
{
    private readonly AppDbContext _context;

    public MaterialController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new MaterialIndexViewModel
        {
            Materials = await _context.Materials
                .Where(material => material.IsDecoration == false)
                .OrderBy(material => material.Name)
                .Select(material => new MaterialViewModel
            {
                Id = material.Id,
                Name = material.Name,
                Unit = material.Unit,
                Price = material.Price,
                Supplier = material.Supplier
            }).ToListAsync(),

            Decorations = await _context.Materials
                .Where(material => material.IsDecoration == true)
                .OrderBy(material => material.Name)
                .Select(material => new MaterialViewModel
            {
                Id = material.Id,
                Name = material.Name,
                Unit = material.Unit,
                Price = material.Price,
                Supplier = material.Supplier
            }).ToListAsync(),
        };

        return View(model);
    }

    [HttpGet("add")]
    public IActionResult AddMaterial() => View(new AddMaterialViewModel());

    [HttpPost("add")]
    public async Task<IActionResult> AddMaterial(AddMaterialViewModel model)
    {   
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var exists = await _context.Materials.Where(m => m.Name == model.Name).SingleOrDefaultAsync();
        if (exists != null)
        {
            string message = $"Materialet {model.Name} finns redan";
            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }

        var newMaterial = new Material
        {
            Name = model.Name,
            Unit = model.Unit,
            Price = model.Price,
            Supplier = model.Supplier,
            IsDecoration = model.IsDecoration
        };

        await _context.Materials.AddAsync(newMaterial);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/edit")]
    public async Task<IActionResult> EditMaterial(int id)
    {
        var material = await _context.Materials.Where(m => m.Id == id).SingleOrDefaultAsync();
        if (material == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var model = new EditMaterialViewModel
        {
            Id = material.Id,
            Name = material.Name,
            Unit = material.Unit,
            Price = material.Price,
            Supplier = material.Supplier,
            IsDecoration = material.IsDecoration
        };

        return View(model);
    }
    
    [HttpPost("{id:int}/edit")]
    public async Task<IActionResult> EditMaterial(int id, EditMaterialViewModel model)
    {
        var material = await _context.Materials.Where(m => m.Id == id).SingleOrDefaultAsync();
        if (material == null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        material.Name = model.Name;
        material.Unit = model.Unit;
        material.Price = model.Price;
        material.Supplier = model.Supplier;
        material.IsDecoration = model.IsDecoration;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id:int}/remove")]
    public async Task<IActionResult> RemoveMaterial(int id)
    {
        var material = await _context.Materials.Where(m => m.Id == id).SingleOrDefaultAsync();
        if (material == null)
        {
            return RedirectToAction(nameof(Index));
        }

        _context.Materials.Remove(material);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
