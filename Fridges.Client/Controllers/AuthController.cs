using Fridges.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Fridges.Client.Models.DTOs;

namespace Fridges.Client.Controllers;

[Route("/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("register")]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto user)
    {
        if (ModelState.IsValid)
        {
            string errorMessage = _authService.Register(user).Result;

            if (String.IsNullOrEmpty(errorMessage))
            {
                return RedirectToAction("GetAllFridges", "Fridge");
            }

            ViewData["ErrorMessage"] = errorMessage;
            return View(user);
        }

        return View(user);
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto user)
    {
        if (ModelState.IsValid)
        {
            string errorMessage = _authService.Login(user).Result;

            if (String.IsNullOrEmpty(errorMessage))
            {
                return RedirectToAction("GetAllFridges", "Fridge");
            }

            ViewData["ErrorMessage"] = errorMessage;
            return View(user);
        }

        return View(user);
    }

    //мб POST
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        _authService.Logout();

        return View("Login");
    }
}
