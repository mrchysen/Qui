using Core.Users.Services;
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
    private IUserRepository _userRepository;
    
    private const string ReturnUrlKey = "ReturnUrl";

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("")]
    public IActionResult LoginPage([FromQuery] string? returnUrl)
    {
        if(returnUrl is not null)
            Response.Cookies.Append(ReturnUrlKey, returnUrl); 

        return View("LoginPage");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromForm] UserLoginDto userDto)
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
    [HttpGet("users/list")]
    public async Task<IActionResult> UserList()
    {
        var users = await _userRepository.GetUsers();

        return View("UserList", new UserListDto()
        {
            Users = users
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("users/delete")]
    public async Task<IActionResult> DeleteUser([FromQuery] Guid id)
    {
        await _userRepository.DeleteUser(id);

        return RedirectToAction("UserList", "Users");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("users/delete-all")]
    public async Task<IActionResult> DeleteAllUsers([FromQuery] Guid id)
    {
        await _userRepository.DeleteAllUser();

        return RedirectToAction("UserList", "Users");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> UserList([FromQuery] Guid id)
    {
        var user = await _userRepository.GetUser(id);

        if(user == null) 
            return RedirectToAction("UserList", "Users");

        return View("User", new UserDto()
        {
            User = user
        });
    }
}
