using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Hattmakare.Data.Entities;
using Hattmakare.Models.Auth;
using Hattmakare.Models.User;

namespace Hattmakare.Controllers;

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
