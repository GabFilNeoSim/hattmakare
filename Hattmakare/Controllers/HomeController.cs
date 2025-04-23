using System.Security.Claims;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Hattmakare.Models;
using Hattmakare.Models.Home;
using Hattmakare.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _appDbContext;
    public HomeController(AppDbContext appDbContext, UserManager<User> userManager)
    {
        _userManager = userManager;
        _appDbContext = appDbContext;
    }

    public async Task<IActionResult> Index()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var model = new HomeIndexViewModel
        {
            LoggedInUser = new UserViewModel
            {
                FirstName = user.FirstName
            }
        };

        return View(model);
    }

    [HttpGet]
    public JsonResult PopulateCalendar()
    {
        var events = _appDbContext.Orders
            .GroupBy(o => o.EndDate)
            .Select(g => new
            {
                Title = $"{g.Count()} ordrar",
                Start = g.Key.ToString("yyyy-MM-dd"),
            }).ToList();

        return Json(events);
    }

    [HttpGet]
    public async Task<IActionResult> PopulateCalendarPopUp(DateTime date)
    {
        var orders = await _appDbContext.Orders
        .Where(o => o.EndDate == date)
        .Select(x => new CalendarPopupViewModel
        {
            Id = x.Id,
            CustomerName = $"{x.Customer.FirstName} {x.Customer.LastName}",
            StartDate = x.StartDate.ToString("MM/dd/yyyy"),
            EndDate = x.EndDate.ToString("MM/dd/yyyy"),
        })
        .ToListAsync();

        return PartialView("_CalendarOrdersPopupPartial", orders);
    }
}
