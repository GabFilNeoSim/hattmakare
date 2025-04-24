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

        public async Task<ActionResult> Index(string? customerId, string? hatId)
        {
            var model = new StatisticsViewModel
            {
                CustomerId = customerId ?? "",
                HatId = hatId ?? "",
                DailyLabels = new List<string>(),
                DailySales = new List<int>(),
                QuarterlySales = new List<int>(),
                MonthlySales = new List<int>()
            };

            var customerList = await _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FirstName + " " + c.LastName,    
                })
                .ToListAsync();

            customerList.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "Alla kunder",
                Selected = true
            });

            var hatList = await _context.Hats
                .Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.Name,
                })
                .ToListAsync();

            hatList.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "Alla hattar",
                Selected = true
            });

            model.Customers = customerList;
            model.Hats = hatList;

            return View(model);
        }


        //public async Task<ActionResult> Index()
        //{

        //    var customerList = await _context.Customers

        //    .Select(c => new SelectListItem
        //    {
        //        Value = c.Id.ToString(),
        //        Text = c.FirstName + " " + c.LastName
        //    })
        //    .ToListAsync();

        //    customerList.Insert(0, new SelectListItem
        //    {
        //        Value = "",
        //        Text = "Alla kunder",
        //        Selected = true
        //    });

        //    var hatList = await _context.Hats
        //        .Select(h => new SelectListItem
        //        {
        //            Value = h.Id.ToString(),
        //            Text = h.Name
        //        })
        //        .ToListAsync();

        //    hatList.Insert(0, new SelectListItem
        //    {
        //        Value = "",
        //        Text = "Alla hattar",
        //        Selected = true
        //    });

        //    var model = new StatisticsViewModel
        //    {
        //        CustomerId = "",
        //        HatId = "",
        //        Customers = customerList,
        //        Hats = hatList,
        //        DailyLabels = new List<string>(), 
        //        DailySales = new List<int>(),
        //        QuarterlySales = new List<int>(),
        //        MonthlySales = new List<int>()
        //    };

        //    return View(model);
        //}

        [HttpGet("data")]
        public async Task<IActionResult> GetChartData(string? customerId, string? hatId)
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderHats)
                .ThenInclude(oh => oh.Hat)
                .ToListAsync();

            int? parsedCustomerId = int.TryParse(customerId, out var cId) && cId != 0 ? cId : null;
            int? parsedHatId = int.TryParse(hatId, out var hId) && hId != 0 ? hId : null;

            if (parsedCustomerId.HasValue)
            {
                orders = orders.Where(o => o.Customer.Id == parsedCustomerId.Value).ToList();
            }

            if (parsedHatId.HasValue)
            {
                orders = orders.Where(o => o.OrderHats.Any(oh => oh.Hat.Id == parsedHatId.Value)).ToList();
            }

            var today = DateTime.Today;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var dailySales = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(i =>
                {
                    var date = startDate.AddDays(i).Date;
                    return new
                    {
                        Date = date.ToString("yyyy-MM-dd"),
                        Total = orders
                            .SelectMany(o => o.OrderHats)
                            .Where(oh => oh.Order.EndDate.Date == date && (!parsedHatId.HasValue || oh.Hat.Id == parsedHatId.Value))
                            .Count()
                    };
                })
                .ToList();

            var quarterlySales = new int[4];
            foreach (var oh in orders.SelectMany(o => o.OrderHats))
            {
                if (!parsedHatId.HasValue || oh.Hat.Id == parsedHatId.Value)
                {
                    var month = oh.Order.EndDate.Month;
                    int quarter = (month - 1) / 3;
                    quarterlySales[quarter]++;
                }
            }

            var monthlySales = new int[12];
            foreach (var oh in orders.SelectMany(o => o.OrderHats))
            {
                if (!parsedHatId.HasValue || oh.Hat.Id == parsedHatId.Value)
                {
                    var month = oh.Order.EndDate.Month;
                    monthlySales[month - 1]++;
                }
            }

            return Json(new
            {
                dailyLabels = dailySales.Select(d => d.Date),
                dailySales = dailySales.Select(d => d.Total),
                quarterlySales,
                monthlySales
            });
        }

    }

}
