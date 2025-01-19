using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.AdminAccess.Authorization;
using Core.AdminAccess;
using RazorPagesApp.Controllers.AdminAccess.Models;

namespace RazorPagesApp.Controllers.AdminAccess;

[Route("admin")]
public class AdminAccessController : Controller
{
    private readonly IAdminRegistrationService _adminRegistrationService;

    public AdminAccessController(IAdminRegistrationService AdminRegistrationService)
    {
        _adminRegistrationService = AdminRegistrationService;
    }

    [HttpGet("login")]
    public IActionResult GetLoginPage() => View("LoginPage", new AdminDataDto());

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAdmin([FromForm] AdminDataDto adminDataDto)
    {
        if (await _adminRegistrationService.IsAdmin(new AdminRegistration()
        {
            Login = adminDataDto.Login,
            Password = adminDataDto.Password
        }))
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = false });

            return RedirectToAction("GetList", "Questions");
        }

        return View("LoginPage", new AdminDataDto()
        {
            Warning = "Неверный пароль или логин"
        });
    }

    [HttpGet("me")]
    public string GetMe() => HttpContext.User.IsInRole("Admin").ToString();
}
