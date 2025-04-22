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
        public async Task<ActionResult> Index(int? customerId, int? hatId)
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
                //.Where(c => !c.IsDeleted)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FirstName + " " + c.LastName
                })
                .ToListAsync();

            var hatList = await _context.Hats
                //.Where(h => !h.IsDeleted)
                .Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.Name
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

            var today = DateTime.Today;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Daglig försäljning för linjediagrammet
            var dailySales = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(i =>
                {
                    var date = startDate.AddDays(i).Date;
                    return new
                    {
                        Date = date.ToString("yyyy-MM-dd"),
                        Total = orders
                            .SelectMany(o => o.OrderHats)
                            .Where(oh => oh.Order.StartDate == DateOnly.FromDateTime(date))
                            .Count()
                    };
                })
                .ToList();

            var model = new StatisticsViewModel
            {
                CustomerId = customerId,
                Customers = customerList,
                HatId = hatId,
                Hats = hatList,
                HatNames = result.Select(r => r.HatName).ToList(),
                Sales = result.Select(r => r.TotalSold).ToList(),

                DailyLabels = dailySales.Select(d => d.Date).ToList(),
                DailySales = dailySales.Select(d => d.Total).ToList()
            };

            return View(model);
        }

    }

}
