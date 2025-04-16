using Hattmakare.Data;
using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Hattmakare.Data.Entities;

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

        var viewModel = await _context.OrderStatuses.Select(x => new OrderListViewModel
        {
            Id = x.Id,
            Status = x.Name,
            Orders = x.Orders.Select(y => new OrderViewModel
            {
                Id = y.Id,
                Customer = y.Customer.FirstName + " " + y.Customer.LastName,
                StartDate = y.StartDate,
                EndDate = y.EndDate,
                Price = y.Price,
                Priority = y.Priority,
                Status = y.OrderStatus.Name,
                Managers = y.OrderHats.Select(z => $"{z.User.FirstName[0].ToString().ToUpper()}{z.User.LastName[0].ToString().ToUpper()}")
                .Distinct()
                .ToList(),
            }).ToList()
        }).ToListAsync();

        return View(viewModel);
    }

    [HttpPost("{oid}/status")]
    public async Task<IActionResult> EditOrderStatus(int oid, int sid)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == oid);
        if (order == null)
            return NotFound();

        var status = await _context.OrderStatuses.SingleOrDefaultAsync(x => x.Id == sid);
        if (status == null)
            return NotFound();

        order.OrderStatusId = sid;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet("edit")]
    public async Task<IActionResult> PopulateEditOrderPopup(int oid)
    {
        var order = await _context.Orders.Where(x => x.Id == oid).Select(x => new OrderViewModel
        {
            Id = x.Id,
            Customer = x.Customer.FirstName + "" + x.Customer.LastName,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Price = x.Price,
            Priority = x.Priority,
            Status = x.OrderStatus.Name
        }).FirstOrDefaultAsync();

        if (order is null) return NotFound();
        return PartialView("_EditOrderPopupPartial", order);
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
