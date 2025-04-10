using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Hattmakare.Models.Auth;

namespace Hattmakare.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(
        AppDbContext context, 
        UserManager<User> userManager, 
        SignInManager<User> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager; 
    }

    [HttpGet("register")]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError(string.Empty, "En användare med samma epost finns redan");
            return View(model);
        }

        User newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(newUser, model.Password);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Gick ej att skapa kontot");
            return View(model);
        }

        await _signInManager.SignInAsync(newUser, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }
}