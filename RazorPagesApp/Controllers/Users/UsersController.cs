using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuiApp.WebMVC.Controllers.Users.Models;
using System.Security.Claims;

namespace QuiApp.WebMVC.Controllers.Users;

[Route("")]
public class UsersController : Controller
{
    private const string ReturnUrlKey = "ReturnUrl";

    [HttpGet("")]
    public IActionResult LoginPage([FromQuery] string? returnUrl)
    {
        if(returnUrl is not null)
            Response.Cookies.Append(ReturnUrlKey, returnUrl); 

        return View("LoginPage");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromForm] UserDto userDto)
    {
        var returnUrl = Request.Cookies[ReturnUrlKey];

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.GivenName, userDto.Name),
            new Claim(ClaimTypes.Surname, userDto.Sername),
            new Claim(ClaimTypes.DateOfBirth, userDto.Age.ToString()),
            new Claim(ClaimTypes.Gender, userDto.Sex.ToString()),
            new Claim(ClaimTypes.Role, "User")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Создаем ClaimsPrincipal
        var principal = new ClaimsPrincipal(identity);

        // Выполняем вход в систему
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = false });

        ViewData["Name"] = userDto.Name;

        Response.Cookies.Delete(ReturnUrlKey);
        return Redirect(returnUrl ?? "/quiz/info");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult UserList()
    {



        return View("UserList", new UserListDto()
        {

        });
    }
}
