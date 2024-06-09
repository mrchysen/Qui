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

        public int RightAnswers = 0;

        protected IUserCRUD Saver;

        public ResultModel(IHttpContextAccessor accessor, IUserCRUD saver, IQuestionHandler questions)
        {
            Console.WriteLine("start create ResultPage");
            var httpContext = accessor.HttpContext;
            Console.WriteLine("create httpContext");
            User = httpContext.Session.Get<User>("user");
            Console.WriteLine("succsesfully get user");
            Progress = httpContext.Session.Get<UserProgress>("progress");
            Console.WriteLine("succsesfully get progress");
            Questions = questions;

            Saver = saver;
            Console.WriteLine("add all entities");

            RightAnswers = Progress.CountRightAnswers(Questions);
            Console.WriteLine("Counted Right answers");
            Console.WriteLine("end with creating ResultPage");
        }

        public IActionResult OnGet()
        {
            Console.WriteLine("Start OnGet met");
            SaveResultAndResetSession();
            Console.WriteLine("Succsesfully save Rusults");

            return Page();
        }

        protected void SaveResultAndResetSession()
        {
            User.Progress = Progress;
            User.Progress.CountRightAnswers(Questions);
            Console.WriteLine("Added Progress to user");

            Console.WriteLine("start to save user into bd");
            Saver.SaveUser(User);
            Console.WriteLine("end with saving");

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
}
