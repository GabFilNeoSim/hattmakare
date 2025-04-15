using System.Diagnostics;
using Hattmakare.Data;
using Hattmakare.Models;
using Hattmakare.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;
        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PopulateCalendar()
        {
            // Todo: lägg till villkor där den inloggde användarens ordrar visas
            var events = _appDbContext.Orders
                .GroupBy(o => o.EndDate)
                .Select(g => new
                {
                    Title = $"{g.Count()} ordrar",
                    Start = g.Key.ToString("yyyy-MM-dd")
                }).ToList();

            return Json(events);
        }

        [HttpGet]
        public async Task<IActionResult> PopulateCalendarPopUp(DateOnly date)
        {
            // Todo: samma som ovan
            var orders = await _appDbContext.Orders
            .Where(o => o.EndDate == date)
            .Select(x => new CalendarPopupViewModel
            {
                Id = x.Id,
                CustomerName = $"{x.Customer.FirstName} {x.Customer.LastName}",
                StartDate = x.StartDate,
                EndDate = x.EndDate,
            })
            .ToListAsync();

            return PartialView("_CalendarOrdersPopupPartial", orders);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
