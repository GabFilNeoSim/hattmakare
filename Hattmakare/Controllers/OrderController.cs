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

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders.Select(x => new OrderViewModel
        {
            Id = x.Id,
            Customer = x.Customer.FirstName + " " + x.Customer.LastName,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Price = x.Price,
            Priority = x.Priority,
            Status = x.OrderStatus.Name,
            Managers = x.OrderHats.Select(y => $"{y.User.FirstName[0]}{y.User.LastName[0]}")
                .Distinct()
                .ToList(),
        }).ToListAsync();

        var orderList = new OrderListViewModel
        {
            Orders = orders
        };

        return View(orderList);
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


    

 
}
