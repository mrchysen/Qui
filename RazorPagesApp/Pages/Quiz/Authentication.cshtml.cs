using Core.UserProgressFeatures;
using Core.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Extensions;

namespace RazorPagesApp.Pages.Quiz;

/// <summary>
/// Authentication page
/// </summary>
public class AuthenticationModel : PageModel
{
    public ISession Session => HttpContext.Session;
    [BindProperty]
    public User? User { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    /// <summary>
    /// OnPost
    /// Handling new User
    /// Redirect to QuizPart
    /// </summary>
    /// <param name="Sex"></param>
    public void OnPostAuthorize(int Sex)
    {
        // Configuring user
        User.Sex = (Sex)Sex;
        User.Id = Guid.NewGuid();

        // Work with Session \\
        if(Session.GetString("authentication") != "admin")
            Session.SetString("authentication", "end");

        if (User.FatherName == null)
            User.FatherName = "-";

        Session.Set("user", User);
        Session.Set("progress", new UserProgress());

        // Redirect
        HttpContext.Response.Redirect("/Quiz/Instructions");
    }
}
