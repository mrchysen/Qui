using Core.Questions;
using Core.Questions.QuestionServices;
using Core.UserProgressFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Quiz;

public class QuestionModel : PageModel
{
    public Question Question;
    public UserProgress Progress { get; set; }
    protected IQuestionHandler Questions { get; set; }
    public QuestionModel(IQuestionHandler questions, IHttpContextAccessor accessor)
    {
        Questions = questions;

    }

    public IActionResult OnPost()
    { 
        ProgressHandling(HttpContext.Request.Form);



        return Page();
    }

    protected void ProgressHandling(IFormCollection? form)
    {
        DateTime startDate = ParseDate(form["starttime"]);
        DateTime endDate = ParseDate(form["endtime"]);

        Progress.Answers.Add(form["answer"]);
        Progress.RightAnswerList.Add(Questions[- 1].IsAnswer(form["answer"]));
        Progress.WasSearched.Add(form["wassearched"] == "1");
        Progress.AnswerStartDateTime.Add(startDate);
        Progress.AnswerEndDateTime.Add(endDate);
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
        

        return Page();
    }
}
