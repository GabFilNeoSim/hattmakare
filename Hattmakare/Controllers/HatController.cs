﻿using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Hats;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Hattmakare.Controllers;

[Authorize]
[Route("hats")]
public class HatController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HatController> _logger;

    public HatController(AppDbContext context, ILogger<HatController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var hats = await _context.Hats
             .Where(x => !x.IsDeleted)
             .Select(x => new HatViewModel
        {
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            Hid = x.Id,
            
            
        }).ToListAsync();
       
        //var hats = new List<HatViewModel>
        //{
        //    new HatViewModel { Name = "Hat 1", ImageUrl = "500x500_placeholder.png" },
        //    new HatViewModel { Name = "Hat 2", ImageUrl = "500x500_placeholder.png" },
        //    new HatViewModel { Name = "Hat 3", ImageUrl = "500x500_placeholder.png" },
        //    new HatViewModel { Name = "Hat 4", ImageUrl = "500x500_placeholder.png" },
        //    new HatViewModel { Name = "Hat 5", ImageUrl = "500x500_placeholder.png" },
        //    new HatViewModel { Name = "Hat 6", ImageUrl = "500x500_placeholder.png" }
        //};
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
        return RedirectToAction("Index");

        //return View(newHat);
    }

    [HttpGet("EditHat/{hid}")]
    public async Task<IActionResult> EditHat(int Hid)
    {
        var hat = await _context.Hats.FirstOrDefaultAsync(x => x.Id == Hid);

        var model = new EditHatViewModel
        {

            
            Name = hat.Name

        };
        return View(model);
    }

    [HttpPost("EditHat/{hid}")]

    public async Task<IActionResult> EditHat(EditHatViewModel selectedHat)
    {
        var hat = await _context.Hats.FirstOrDefaultAsync(x => x.Id == selectedHat.Hid);
        hat.Name = selectedHat.Name;
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
        hat.IsDeleted= true;
        
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
            Hid = hat.Id,
            Name = hat.Name,
            ImageUrl = hat.ImageUrl,
            IsDeleted = hat.IsDeleted
        }).ToList();

        return View("Index", model);  
    }


}
