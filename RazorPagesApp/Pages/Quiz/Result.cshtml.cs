using Core.Questions.QuestionServices;
using Core.UserProgressFeatures;
using Core.Users;
using Core.Users.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Quiz;

public class ResultModel : PageModel
{
    public User User { get; protected set; }
    public UserProgress Progress { get; protected set; }
    public IQuestionHandler Questions { get; protected set; }

    public int RightAnswers = 0;

    protected IUserRepository Saver;

    public ResultModel(IHttpContextAccessor accessor, IUserRepository saver, IQuestionHandler questions)
    {
        var httpContext = accessor.HttpContext;

        Questions = questions;

        Saver = saver;
    }

    public IActionResult OnGet()
    {
        SaveResultAndResetSession();

        return Page();
    }

    protected void SaveResultAndResetSession()
    {
        User.Progress = Progress;
        User.Progress.Id = Guid.NewGuid();

        if (HttpContext.Session.GetString("authentication") != "admin")
        {
            HttpContext.Session.Clear();
        }
    }
    protected string QueestionText()
    {
        if (RightAnswers % 10 == 1) return "";
        if (RightAnswers % 10 > 1 && RightAnswers % 10 < 5) return "a";
        return "ов";
    }
}
