using Core.Questions.QuestionServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Administration;

public class QuestionsModel : PageModel
{
    public IQuestionHandler Questions { get; set; }

    public QuestionsModel(IQuestionHandler questions) 
    {
        Questions = questions;
    }

    #region Arrow methods - allow to swap questions
    public IActionResult OnGetUp(int index)
    {
        if (index >= Questions.Count)
            return Redirect("/Administration/Questions");

        var tempQuestion = Questions[index];
        Questions.ChangeQuestion(index, Questions[index - 1]);
        Questions.ChangeQuestion(index - 1, tempQuestion);

        return Redirect("/Administration/Questions");
    }

    public IActionResult OnGetDown(int index)
    {
        if (index >= Questions.Count)
            return Redirect("/Administration/Questions");

        var tempQuestion = Questions[index];
        Questions.ChangeQuestion(index, Questions[index + 1]);
        Questions.ChangeQuestion(index + 1, tempQuestion);

        return Redirect("/Administration/Questions");
    }
    #endregion

    /// <summary>
    /// Remove all questions
    /// </summary>
    /// <returns></returns>
    public IActionResult OnGetClear()
    {
        Questions.DeleteAllQuestions();

        return Redirect("/Administration/Questions");
    }
    /// <summary>
    /// Remove one question
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IActionResult OnGetDelete(int index)
    {
        Questions.DeleteQuestion(index);

        return Redirect("/Administration/Questions");
    }
}
