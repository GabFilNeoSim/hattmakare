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
using Hattmakare.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Hattmakare.Models.Customer;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hattmakare.Controllers;
[Authorize]
[Route("order")]
public class OrderController : Controller 
{
    private readonly AppDbContext _context;
    private readonly IImageService _imageService;
    private ILogger<OrderController> _logger;
    

    public OrderController(AppDbContext context, IImageService imageService, ILogger<OrderController> _logger)
    {
        _context = context;
        _imageService = imageService;
        _logger = _logger;
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
    public async Task<IActionResult> Index(string searchQuery, int? HatId, DateTime? StartDate, DateTime? EndDate)
    {
        var hatNameList = await _context.HatTypes
         .Select(x => new SelectListItem
         {
             Value = x.Id.ToString(),
             Text = x.Name,
         })
         .ToListAsync();


        var filteredOrders = _context.Orders
      .Include(o => o.Customer)
      .Include(o => o.OrderStatus)
      .Include(o => o.OrderHats).ThenInclude(oh => oh.Hat)
      .Include(o => o.OrderHats).ThenInclude(oh => oh.User)
      .AsQueryable();

        if (StartDate.HasValue)
        {
            filteredOrders = filteredOrders.Where(o => o.StartDate >= StartDate.Value);
        }

        if (EndDate.HasValue)
        {
            filteredOrders = filteredOrders.Where(o => o.EndDate <= EndDate.Value);
        }


        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            filteredOrders = filteredOrders.Where(o =>
                (o.Customer.FirstName + " " + o.Customer.LastName).ToLower().Contains(searchQuery.ToLower()));
        }

        if (HatId.HasValue)
        {
            filteredOrders = filteredOrders.Where(o =>
                o.OrderHats.Any(h => h.Hat.HatTypeId == HatId));
        }

        var ordersQuery = await _context.OrderStatuses
            .Select(s => new OrderListViewModel
            {
                Id = s.Id,
                Status = s.Name,
                Orders = filteredOrders
                    .Where(o => o.OrderStatusId == s.Id)
                    .Select(y => new OrderViewModel
                    {
                        Id = y.Id,
                        Customer = y.Customer.FirstName + " " + y.Customer.LastName,
                        StartDate = y.StartDate.ToString("MM/dd/yyyy"),
                        EndDate = y.EndDate.ToString("MM/dd/yyyy"),
                        Price = y.Price,
                        Priority = y.Priority,
                        Status = y.OrderStatus.Name,
                        Managers = y.OrderHats
                            .Where(z => z.User != null)
                            .Select(z => $"{z.User.FirstName[0].ToString().ToUpper()}{z.User.LastName[0].ToString().ToUpper()}")
                            .Distinct()
                            .ToList(),
                    }).ToList()
            })
            .ToListAsync();


        var viewModel = new OrderIndexViewModel
        {
            HatNames = hatNameList,
            HatId = HatId ?? 0,
            StartDate = StartDate,
            EndDate = EndDate,
            OrderItems = ordersQuery
        };


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
            StartDate = order.StartDate.ToString("MM/dd/yyyy"),
            EndDate = order.EndDate.ToString("MM/dd/yyyy"),
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

        order.StartDate = DateTime.Parse(request.StartDate);
        order.EndDate = DateTime.Parse(request.EndDate);
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
    public async Task<IActionResult> New()
    {
        var model = new NewOrderIndexViewModel
        {
            Hats = await _context.Hats
                .Where(h => h.IsDeleted == false && h.HatType.Name == "StandardHatt")
                .Select(x => new HatViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Size = x.Size,
                    Comment = x.Comment,
                    ImageUrl = x.ImageUrl
                })
                .ToListAsync(),

            Customers = await _context.Customers
                .Where(c => c.IsDeleted == false)
                .OrderBy(c => c.FirstName)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FirstName + " " + c.LastName
                })
                .ToListAsync()
        };

        return View(model);
    }

  [HttpPost("AddSpecialHat")]
  public async Task<IActionResult> AddSpecialHat([FromForm] AddHatViewModel newHat)
  {

    var hat = new Hat();

    hat.Name = newHat.Name;
    hat.Size = newHat.Size ?? 0;
    hat.Length = newHat.Length ?? 0;
    hat.Depth = newHat.Depth ?? 0;
    hat.Width = newHat.Width ?? 0;
    hat.Quantity = newHat.Quantity;
    hat.Price = newHat.Price ?? 0;
    hat.Comment = newHat.Comment ?? "";
    hat.HatMaterials = new List<HatMaterial>();
    hat.HatType = await _context.HatTypes
        .FirstOrDefaultAsync(x => x.Name == "SpecialHatt");


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

    return Ok(hat.Id);
  }

    [HttpGet("get-customer/{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _context.Customers
         .Include(c => c.Address)
         .OrderBy(c => c.FirstName)
         .FirstOrDefaultAsync(c => c.Id == id);

        if (customer.IsDeleted == false)
        {
            return Json(new
            {
                firstName = customer.FirstName,
                lastName = customer.LastName,
                headMeasurements = customer.HeadMeasurements,
                email = customer.Email,
                phone = customer.PhoneNumber,
                billingAddress = customer.Address?.BillingAddress,
                deliveryAddress = customer.Address?.DeliveryAddress,
                city = customer.Address?.City,
                postalCode = customer.Address?.PostalCode,
                country = customer.Address?.Country
            });
        }
        else
        {
            return NotFound();
        }
        
    }

    [HttpPost("new")]
    public async Task<IActionResult> CreateOrder(NewOrderIndexViewModel viewModel)
    {
        Customer customer;

        if (viewModel.CustomerId > 0)
        {
            customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == viewModel.CustomerId);

            if (customer == null)
            {
                TempData["NotifyType"] = "error";
                TempData["NotifyMessage"] = "Kunden hittades inte!";
                return RedirectToAction("New");
            }

        }
        else
        {

            var address = new Address
            {
                BillingAddress = viewModel.AddCustomer.BillingAddress,
                DeliveryAddress = viewModel.AddCustomer.DeliveryAddress,
                City = viewModel.AddCustomer.City,
                PostalCode = viewModel.AddCustomer.PostalCode,
                Country = viewModel.AddCustomer.Country
            };

            customer = new Customer
            {
                FirstName = viewModel.AddCustomer.FirstName,
                LastName = viewModel.AddCustomer.LastName,
                HeadMeasurements = viewModel.AddCustomer.HeadMeasurements,
                Email = viewModel.AddCustomer.Email,
                PhoneNumber = viewModel.AddCustomer.Phone,

            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        var order = new Order
        {
            Priority = viewModel.NewOrders.Priority,
            StartDate = viewModel.NewOrders.StartDate,
            EndDate = viewModel.NewOrders.EndDate,
            Price = viewModel.NewOrders.Price,
            CustomerId = customer.Id
        };

        var orderStatus = new OrderStatus
        {
            Id = viewModel.OrderStatusId,
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
        
    }

}
