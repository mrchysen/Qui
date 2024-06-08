using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using RazorPagesApp.Extensions;
using RazorPagesApp.Services.Questions;
using RazorPagesApp.Services.Progress;

namespace RazorPagesApp.Pages.Quiz
{
    public class ResultModel : PageModel
    {
        public User User { get; protected set; }
        public UserProgress Progress { get; protected set; }
        public IQuestionHandler Questions { get; protected set; }
        public string ResultText => $"Вы ответили на {RightAnswers} вопрос{QueestionText()} правильно.";

        public int RightAnswers = 0;

        protected IUserCRUD Saver;

        public ResultModel(IHttpContextAccessor accessor, IUserCRUD saver, IQuestionHandler questions)
        {
            var httpContext = accessor.HttpContext;

            User = httpContext.Session.Get<User>("user");
            Progress = httpContext.Session.Get<UserProgress>("progress");
            Questions = questions;

            Saver = saver;

            RightAnswers = Progress.CountRightAnswers(Questions);
        }

        public IActionResult OnGet()
        {
            SaveResultAndResetSession();

            return Page();
        }

        protected void SaveResultAndResetSession()
        {
            User.Progress = Progress;
            User.Progress.CountRightAnswers(Questions);

            Saver.SaveUser(User);

            if(HttpContext.Session.GetString("authentication") != "admin")
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
}
