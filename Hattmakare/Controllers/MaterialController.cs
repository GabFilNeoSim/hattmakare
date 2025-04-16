using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hattmakare.Data;
using Hattmakare.Models.Material;
using Microsoft.EntityFrameworkCore;

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

    [HttpPost]
    public IActionResult AddMaterial(AddMaterialViewModel model)
    {
        return View();
    }

    [HttpPost]
    public IActionResult EditMaterial(EditMaterialViewModel model)
    {
        return View();
    }

    [HttpPost("{id:int}")]
    public IActionResult RemoveMaterial(int id)
    {
        return View();
    }
}
