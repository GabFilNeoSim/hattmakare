using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Hattmakare.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Controllers;

[Authorize(Roles = "Admin")]
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

    [HttpGet]
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
                x => x.userRoles.DefaultIfEmpty(),
                (x, userRole) => new { x.user, userRole })
            .GroupJoin(_context.Roles,
                combined => combined.userRole.RoleId,
                role => role.Id,
                (combined, roles) => new { combined.user, roles })
            .SelectMany(
                x => x.roles.DefaultIfEmpty(),
                (x, role) => new UserViewModel
                {
                    Id = x.user.Id,
                    FirstName = x.user.FirstName,
                    LastName = x.user.LastName,
                    Email = x.user.Email!,
                    Role = role != null ? role.Name : "Ingen roll"
                }).OrderBy(x => x.Role)
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

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return RedirectToAction(nameof(Index));
        }

        var model = new EditUserViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
        };

        return View(model);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (!ModelState.IsValid)
        {   
            return View(nameof(EditUser), model);
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.UserName = model.Email;

        switch (model.IsAdmin) {
            case true:
                await _userManager.AddToRoleAsync(user, "Admin");
                break;
            case false:
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                break;
        }

        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}/remove")]
    public async Task<IActionResult> RemoveUser(string id)
    {
        var userToRemove = await _userManager.FindByIdAsync(id);
        if (userToRemove == null)
        {
            return RedirectToAction(nameof(Index));
        }

        var loggedInUser = await _userManager.GetUserAsync(User);
        if (userToRemove.Id == loggedInUser!.Id)
        {
            return RedirectToAction(nameof(Index));
        }

        await _userManager.DeleteAsync(userToRemove);

        return RedirectToAction(nameof(Index));
    }
}
