using Fridges.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;

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
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
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

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        _authService.RefreshToken();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("give-roles")]
    public async Task<IActionResult> GiveRoles()
    {
        ViewBag.Users = _authService.GetAllUsers().Result;
        ViewBag.Roles = _authService.GetAllRoles().Result;
        ViewBag.Action = "GiveRoles";

        return View();
    }

    [HttpPost("give-roles")]
    public async Task<IActionResult> GiveRoles(GiveRoleDto giveRoleDto)
    {
        IEnumerable<UserOutputDto> users;
        if (ModelState.IsValid)
        {
            _authService.GiveRoles(giveRoleDto);
            ViewBag.Users = _authService.GetAllUsers().Result;
            ViewBag.Roles = _authService.GetAllRoles().Result;
            ViewBag.Action = "GiveRoles";

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GiveRoles", giveRoleDto) });
        }

        ViewBag.Users = _authService.GetAllUsers().Result;
        ViewBag.Roles = _authService.GetAllRoles().Result;
        ViewBag.Action = "GiveRoles";

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "GiveRoles", giveRoleDto) });
    }
}
