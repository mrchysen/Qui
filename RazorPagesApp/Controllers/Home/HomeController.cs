using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorPagesApp.Extensions;

namespace RazorPagesApp.Controllers.Home;

[Route("")]
[Authorize]
public class HomeController : Controller
{
    private ISession _session => HttpContext.Session;

    [HttpGet("")]
    public IActionResult Index() => View("Auth");

    [HttpGet("Instruction")]
    public IActionResult Instruction() => View("Instruction");

    [HttpPost("CreateQuizSession")]
    public IActionResult CreateQuizSession([FromForm]User user, [FromForm]int Sex)
    {
        // Configuring user
        user.Sex = (Sex)Sex;
        user.Id = Guid.NewGuid();

        // Work with Session \\
        if (_session.GetString("authentication") != "admin")
            _session.SetString("authentication", "end");

        if (user.FatherName == null)
            user.FatherName = "-";

        _session.Set("user", user);
        _session.Set("progress", new UserProgress());

        // Redirect
        return RedirectToAction("Instruction", "Home");
    }
}
