using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Hattmakare.Models.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Hattmakare.Controllers;

//[Authorize]
[Route("auth")]
public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
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

        var newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Email,
            Email = model.Email,
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

        await _signInManager.SignInAsync(newUser, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("login")]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        const string errorMessage = "Fel epost eller lösenord";
        
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user,
            model.Password,
            isPersistent: model.RememberMe,
            lockoutOnFailure: false
        );

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
