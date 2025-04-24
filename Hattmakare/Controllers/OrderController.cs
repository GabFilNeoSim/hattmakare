using Hattmakare.Data;
using Hattmakare.Models.Hats;
using Hattmakare.Models.Material;
using Microsoft.AspNetCore.Mvc;
using Hattmakare.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Hattmakare.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection;

namespace Hattmakare.Controllers;
[Authorize]
[Route("order")]
public class OrderController : Controller 
{
    private readonly AppDbContext _context;
    private readonly ILogger<OrderController> _logger;

    public OrderController(AppDbContext context, ILogger<OrderController> logger)
    {
        _context = context;
        _logger = logger;
    }
   
    [HttpGet("materialorder")]
    public async Task<IActionResult> MaterialOrder(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderHats)
                .ThenInclude(oh => oh.Hat)
                    .ThenInclude(h => h.HatMaterials)
                        .ThenInclude(hm => hm.Material)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            _logger.LogWarning("Ingen order hittades med ID {OrderId}", orderId);
            
        }

        var allHatMaterials = order.OrderHats
            .SelectMany(oh => oh.Hat.HatMaterials)
            .ToList();

        var model = new MaterialOrderViewModel
        {
            OrderId = order.Id,
            HatMaterials = allHatMaterials
        };

        return View(model);
    }


    [HttpGet("waybill")]
    public async Task<IActionResult> Waybill(int orderId)
    {
        var userEmail = User.Identity.Name;
        var sender = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        var order = await _context.Orders
            .Include(o => o.Customer)
            .ThenInclude(c => c.Address) 
            .FirstOrDefaultAsync(o => o.Id == orderId);

        decimal totalPrice = order.OrderHats
        .Where(oh => oh.Hat is Hat) 
        .Sum(oh => ((Hat)oh.Hat).Price);

        if (order == null)
        return Content($"No order found with ID {orderId}");

        var model = new WayBilViewModel
        {
            orderNumber = order.Id,
            address = "Hattmakarvägen 1<br />702 52 Örebro",
            sender = sender,
            reciver = order.Customer,
            IsPriority = order.Priority,
            price = totalPrice,
            weight = 2,
            OrderHats = order.OrderHats.ToList()
        };

        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Index(string searchQuery)
    {
        IQueryable<OrderListViewModel> ordersQuery;

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            
            ordersQuery = _context.OrderStatuses
                .Select(x => new OrderListViewModel
                {
                  Id = x.Id,
                  Status = x.Name,
                  Orders = x.Orders
                  .Where(y =>
                        (y.Customer.FirstName + " " + y.Customer.LastName).Contains(searchQuery.ToLower()))
                        .Select(y => new OrderViewModel 
                        {
                            Id = y.Id,
                            Customer = y.Customer.FirstName + " " + y.Customer.LastName,
                            StartDate = y.StartDate,
                            EndDate = y.EndDate,
                            Price = y.Price,
                            Priority = y.Priority,
                            Status = y.OrderStatus.Name,
                            Managers = y.OrderHats
                                .Where(z => z.User != null)
                                .Select(z => $"{z.User.FirstName[0].ToString().ToUpper()}{z.User.LastName[0].ToString().ToUpper()}")
                                .Distinct()
                                .ToList(),
                        }).ToList()
                });
        }
        else
        {
            
            ordersQuery = _context.OrderStatuses
                .Select(x => new OrderListViewModel
                {
                    Id = x.Id,
                    Status = x.Name,
                    Orders = x.Orders
                        .Select(y => new OrderViewModel
                        {
                            Id = y.Id,
                            Customer = y.Customer.FirstName + " " + y.Customer.LastName,
                            StartDate = y.StartDate,
                            EndDate = y.EndDate,
                            Price = y.Price,
                            Priority = y.Priority,
                            Status = y.OrderStatus.Name,
                            Managers = y.OrderHats
                                .Where(z => z.User != null)
                                .Select(z => $"{z.User.FirstName[0].ToString().ToUpper()}{z.User.LastName[0].ToString().ToUpper()}")
                                .Distinct()
                                .ToList(),
                        }).ToList()
                });
        }

       
        var viewModel = await ordersQuery.ToListAsync();

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

        TempData["NotifyType"] = "success";
        TempData["NotifyMessage"] = "Done";

        return Ok();
    }

    [HttpGet("edit")]
    public async Task<IActionResult> PopulateEditOrderPopup(int oid)
    {
        var order = await _context.Orders.Where(x => x.Id == oid).FirstOrDefaultAsync();

        if (order is null) return NotFound();

        var users = await _context.Users
            .Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.FirstName + " " + u.LastName
            })
            .ToListAsync();

        var viewModel = new EditOrderViewModel
        {
            Id = order.Id,
            CustomerName = order.Customer?.FirstName + " " + order.Customer?.LastName,
            CustomerPhone = order.Customer?.PhoneNumber,
            StartDate = order.StartDate,
            EndDate = order.EndDate,
            Price = order.Price,
            Priority = order.Priority,
            Status = order.OrderStatus?.Name,
            Hats = order.OrderHats.Select(x => new EditOrderHatViewModel
            {
                HatId = x.HatId,
                HatName = x.Hat.Name,
                HatImageUrl = x.Hat.ImageUrl ?? "/assets/hats/placeholder-image.png",
                UserId = x.UserId,
                Depth = x.Hat.Depth,
                Length = x.Hat.Length,
                Price = x.Hat.Price,
                Size = x.Hat.Size,
                Width = x.Hat.Width,
                Materials = x.Hat.HatMaterials.Select(y => new OrderHatMaterialViewModel
                {
                    Name = y.Material.Name,
                    Unit = y.Material.Unit,
                    Amount = y.Quantity
                }).ToList()
            }).ToList(),
            Users = users
        };


        return PartialView("_EditOrderPopupPartial", viewModel);
    }

    [HttpPost("{oid}/edit")]
    public async Task<IActionResult> EditOrder(int oid, EditOrderViewModel request)
    {
        var order = await _context.Orders.Where(x => x.Id == oid).FirstOrDefaultAsync();
        if (order is null) return NotFound();

        order.StartDate = request.StartDate;
        order.EndDate = request.EndDate;
        order.Priority = request.Priority;


        if (request.Hats is not null)
        {
            foreach (var hat in request.Hats)
            {
                var orderHat = order.OrderHats.FirstOrDefault(oh => oh.HatId == hat.HatId);
                if (orderHat != null)
                {
                    orderHat.UserId = hat.UserId;
                }
            }
        }

        await _context.SaveChangesAsync();

        TempData["NotifyType"] = "success";
        TempData["NotifyMessage"] = "Ändringarna för ordern sparades.";

        return Ok();
    }

    [HttpPost("{oid}")]
    public async Task<IActionResult> DeleteOrder(int oid, EditOrderViewModel request)
    {
        var order = await _context.Orders.Where(x => x.Id == oid).FirstOrDefaultAsync();
        if (order is null) return NotFound();

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        TempData["NotifyType"] = "success";
        TempData["NotifyMessage"] = "Ordern togs bort.";
         
        return RedirectToAction("Index");
    }

    [HttpGet("new")]
    public async Task<IActionResult> Hats()
    {
        var model = new NewOrderViewModel
        {
            Hats = await _context.Hats.Where(h => h.IsDeleted == false).Select(x =>
                new HatViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Size = x.Size,
                    Comment = x.Comment,
                    ImageUrl = x.ImageUrl
                }
            ).ToListAsync(),
        };

        return View(model);
    }



    [HttpGet("dropdown-hats")]
    public async Task<IActionResult> DropdownHats()
    {
        var hats = await _context.Hats
            .Include(h => h.HatType)
            .Where(h => !h.IsDeleted)
            .Select(h => new HatViewModel
            {
                Id = h.Id,
                Name = h.Name,
                HatTypeName = h.HatType.Name
            })
            .ToListAsync();

        return PartialView("_DropdownHatsPartial", hats);
    }





}
