using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Qui.Core.Services.Questions;

namespace Qui.Api.Pages.Administration
{
    public class CreateQuestionModel : PageModel
    {
        public IQuestionHandler Questions { get; set; }
        public CreateQuestionModel(IQuestionHandler questions)
        {
            Questions = questions;
        }
        public IActionResult OnPost() 
        {
            var form = HttpContext.Request.Form;

            var text = form["Text"].ToString().Trim();
            var answers = form["Answers"].ToString().Replace("\r", "")
                .Split("\n").Select(elem => elem.ToLower()).ToList();

            Questions.CreateQuestion(new Question()
            {
                Id = Guid.NewGuid(),
                Text = text,
                Answers = answers
            });

            return Redirect("/Administration/Questions");
        }
    }
}
