using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Hattmakare.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Controllers;

[Route("users")]
public class UserController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public UserController(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {   
        var model = new UserIndexViewModel
        {
            Users = await _context.Users
        .GroupJoin(_context.UserRoles,
            user => user.Id,
            userRole => userRole.UserId,
            (user, userRoles) => new { user, userRoles })
        .SelectMany(
            x => x.userRoles.DefaultIfEmpty(), // Vänster join
            (x, userRole) => new { x.user, userRole })
        .GroupJoin(_context.Roles,
            combined => combined.userRole.RoleId,
            role => role.Id,
            (combined, roles) => new { combined.user, roles })
        .SelectMany(
            x => x.roles.DefaultIfEmpty(), // Vänster join
            (x, role) => new UserViewModel
            {
                FirstName = x.user.FirstName,
                LastName = x.user.LastName,
                Email = x.user.Email!,
                Role = role != null ? role.Name : "Ingen roll" // Om ingen roll finns
            })
        .ToListAsync()
        };

        return View(model);
    }

    [HttpGet("add")]
    public IActionResult AddUser() => View(new AddUserViewModel());

    [HttpPost("add")]
    public async Task<IActionResult> AddUser(AddUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(newUser, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        if (model.IsAdmin)
        {
            await _userManager.AddToRoleAsync(newUser, "Admin");
        }

        return RedirectToAction("Index", "User");
    }

    [HttpPost("{id}/remove")]
    public async Task<IActionResult> RemoveUser(string id)
    {   
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return RedirectToAction("Index", "User");
        }

        await _userManager.DeleteAsync(user);

        return RedirectToAction("Index", "User");
    }
}
