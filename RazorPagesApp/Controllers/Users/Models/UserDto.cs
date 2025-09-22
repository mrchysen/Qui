using Core.Users;

namespace QuiApp.WebMVC.Controllers.Users.Models;

public class UserDto
{
    public User User { get; set; }

    public bool IndexCondition(int index)
    {
        return index < User.Progress.Answers.Count &&
            index < User.Progress.RightAnswerList.Count &&
            index < User.Progress.AnswerEndDateTime.Count &&
            index < User.Progress.AnswerStartDateTime.Count &&
            index < User.Progress.WasSearched.Count;
    }
    public string GetStartDate()
    {
        if (User.Progress.AnswerStartDateTime.Count <= 0)
        {
            return "нет";
        }
        return User.Progress.AnswerStartDateTime[0].ToLongDateString();
    }
}
