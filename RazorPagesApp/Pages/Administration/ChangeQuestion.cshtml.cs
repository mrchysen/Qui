using Core.Questions;
using Core.Questions.QuestionServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace RazorPagesApp.Pages.Administration;

public class ChangeQuestionModel : PageModel
{
    protected IQuestionHandler Questions { get; set; }
    public Question? Question { get; set; }
    public ChangeQuestionModel(IQuestionHandler questions)
    {
        Questions = questions;
        Question = Questions[0];
    }


    public IActionResult OnGet(int index)
    {
        if (index >= Questions.Count)
            return Redirect("/Administration/Questions");

        Question = Questions[index];

        ViewData["index"] = index;

        return Page();
    }

    public IActionResult OnPost()
    {
        var form = HttpContext.Request.Form;

        int index = int.Parse((form["Index"] == StringValues.Empty )? "0" : form["Index"]);
        var text = form["Text"].ToString().Trim();
        var answers = form["Answers"].ToString().Replace("\r", "")
            .Split("\n").Select(elem => elem.ToLower()).ToList();

        Questions.ChangeQuestion(index, new Question()
        {
            Text = text,
            Answers = answers
        });

        return Redirect("/Administration/Questions");
    }
}
