using Hattmakare.Data;
using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Hattmakare.Controllers;
[Authorize]
[Route("order")]
public class OrderController : Controller 
{
    private readonly AppDbContext _context;
    private readonly List<StandardHatViewModel> Cart = [];

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("new/hats")]
    public async Task<IActionResult> Hats()
    {
        var model = new OrderHatsViewModel
        {
            Hats = await _context.StandardHats.Where(h => h.IsDeleted == false).Select(x =>
                new StandardHatViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Comment = x.Comment,
                    ImageUrl = x.ImageUrl
                }
            ).ToListAsync(),
            SpecialHats = await _context.SpecialHats.Select(x =>
                new StandardHatViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Comment = x.Comment,
                    ImageUrl = x.ImageUrl
                }
            ).ToListAsync(),
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(StandardHatViewModel hat)
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
