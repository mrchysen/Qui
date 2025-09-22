using Core.Users;

namespace QuiApp.WebMVC.Controllers.Users.Models;

public class UserListDto
{
    public List<User> Users { get; set; }

    public static string GetStartDate(User user)
    {
        if (user.Progress.AnswerStartDateTime.Count <= 0)
        {
            return "нет";
        }
        return user.Progress.AnswerStartDateTime[0].ToLongDateString();
    }
}
