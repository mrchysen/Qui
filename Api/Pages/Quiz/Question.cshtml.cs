using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Qui.Api.Extensions;
using Qui.Core.Models;
using Qui.Core.Services.Questions;

namespace Qui.Api.Pages.Quiz;

public class QuestionModel : PageModel
{
    public Question Question;
    public UserProgress Progress { get; set; }
    protected IQuestionHandler Questions { get; set; }
    public QuestionModel(IQuestionHandler questions, IHttpContextAccessor accessor)
    {
        Questions = questions;
        Progress = accessor.HttpContext.Session.Get<UserProgress>("progress");

        Question = Progress.CurrentQuestion < Questions.Count ? Questions[Progress.CurrentQuestion] : new();
    }

    public IActionResult OnPost()
    { 
        ProgressHandling(HttpContext.Request.Form);

        Question = Progress.CurrentQuestion < Questions.Count ? Questions[Progress.CurrentQuestion] : new();

        if (IsEndOfTesting())
        {
            return Redirect("/Quiz/Result");
        }

        return Page();
    }

    protected void ProgressHandling(IFormCollection? form)
    {
        DateTime startDate = ParseDate(form["starttime"]);
        DateTime endDate = ParseDate(form["endtime"]);

        Progress.CurrentQuestion++;
        Progress.Answers.Add(form["answer"]);
        Progress.IsRightAnswerList.Add(Questions[Progress.CurrentQuestion - 1].IsAnswer(form["answer"]));
        Progress.WasSearched.Add(form["wassearched"] == "1");
        Progress.AnswerStartDateTime.Add(startDate);
        Progress.AnswerEndDateTime.Add(endDate);

        HttpContext.Session.Set("progress", Progress);
    }
    /// <summary>
    /// format "YYYY-MM-DD hh:mm:ss,fff"
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    protected DateTime ParseDate(string date)
    {
        string[] data = date.Split();
        string[] dateStrings = data[0].Split("-");
        string[] timeStrings = data[1].Split(":");
        string[] secondsAndMilliseconds = timeStrings[2].Split(",");

        int seconds = int.Parse(secondsAndMilliseconds[0]);
        int milliseconds = int.Parse(secondsAndMilliseconds[1]);
        int minutes = int.Parse(timeStrings[1]);
        int hours = int.Parse(timeStrings[0]);

        int days = int.Parse(dateStrings[2]);
        int month = int.Parse(dateStrings[1]);
        int years = int.Parse(dateStrings[0]);

        return new DateTime(years,month,days,hours,minutes,seconds,milliseconds);
    }

    public IActionResult OnGet()
    {
        if (IsEndOfTesting())
        {
            return Redirect("/Quiz/Result");
        }

        return Page();
    }

    protected bool IsEndOfTesting()
    {
        return Progress.CurrentQuestion >= Questions.Count;
    }
}
