using Microsoft.AspNetCore.Mvc;

namespace RazorPagesApp.Controllers.UsersAuth;

[Route("users")]
public class UsersAuthController : Controller
{
    [HttpGet("registrate-form")]
    public ViewResult GetAuthForm() => View("Auth");

    [HttpPost("registrate-form")]
    public RedirectResult PostAuthForm([FromForm]RegistrateCustomerFormRequest request, string? returnUrl)
    {


        return Redirect(returnUrl ?? "");
    }
}
