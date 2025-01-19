using Core.Models;
using Core.Services.Authorization;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RazorPagesApp.Controllers.Models;
using System.Security.Claims;
using DAL;

namespace RazorPagesApp.Controllers;

[Route("admin")]
public class AdminAccessController : Controller
{
    private readonly IAdminRegistration _adminRegistration;

    public AdminAccessController(AppDbContext AppDbContext)
    {
        _adminRegistration = AppDbContext;
    }

    [HttpGet("login")]
    public IActionResult GetLoginPage() => View("LoginPage", new AdminDataDto());

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAdmin([FromForm]AdminDataDto adminDataDto)
    {
        if (_adminRegistration.IsAdmin(new Registration()
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

            return Redirect("/Administration/Questions");
        }

        return View("LoginPage", new AdminDataDto()
        {
            Warning = "Неверный пароль или логин"
        });
    }

    [HttpGet("me")]
    public string GetMe() => HttpContext.User.IsInRole("Admin").ToString();
}
