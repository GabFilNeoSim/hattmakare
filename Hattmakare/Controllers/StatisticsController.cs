using Hattmakare.Data;
using Hattmakare.Models.Order;
using Hattmakare.Models.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Controllers
{
    [Authorize]
    [Route("statistics")]
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _context;

        public StatisticsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? customerId)
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderHats)
                .ThenInclude(oh => oh.Hat)
                .ToListAsync();

            if (customerId.HasValue)
            {
                orders = orders
                    .Where(o => o.Customer.Id == customerId.Value)
                    .ToList();
            }

            var customerList = await _context.Customers
                .Where(c => !c.IsDeleted)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FirstName + " " + c.LastName
                })
                .ToListAsync();

            var allOrderHats = orders.SelectMany(o => o.OrderHats);

            var result = allOrderHats
                .GroupBy(oh => oh.Hat.Name)
                .Select(g => new
                {
                    HatName = g.Key,
                    TotalSold = g.Count()
                })
                .ToList();

            var model = new StatisticsViewModel
            {
                CustomerId = customerId,
                Customers = customerList,
                HatNames = result.Select(r => r.HatName).ToList(),
                Sales = result.Select(r => r.TotalSold).ToList()
            };

            return View(model);
        }

    }

}
