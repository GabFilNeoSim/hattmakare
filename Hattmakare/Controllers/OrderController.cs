using Hattmakare.Data;
using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Controllers;

[Route("order")]
public class OrderController : Controller 
{
    private readonly AppDbContext _context;
    private readonly List<HatViewModel2> Cart = [];

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("new/hats")]
    public async Task<IActionResult> Hats()
    {
        var model = new OrderHatsViewModel
        {
            Hats = await _context.Hats.Where(h => h.IsSpecial == false && h.IsDeleted == false).Select(x =>
                new HatViewModel2 {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Comment = x.Comment,
                    ImageUrl = x.ImageUrl
                }
            ).ToListAsync(),
            SpecialHats = await _context.Hats.Where(h => h.IsSpecial == true && h.IsDeleted == false).Select(x =>
                new HatViewModel2
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Comment = x.Comment,
                    ImageUrl = x.ImageUrl
                }
            ).ToListAsync(),
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(HatViewModel2 hat)
    {   
        if (hat != null)
        {
            Cart.Add(hat);
        }

        return View();
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var hatToRemove = Cart.FirstOrDefault(hat => hat.Id == id);
        if (hatToRemove != null)
        {
            Cart.Remove(hatToRemove);
        }

        return View();
    }
}
