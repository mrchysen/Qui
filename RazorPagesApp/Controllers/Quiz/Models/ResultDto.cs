using Core.Questions;
using Core.UserProgressFeatures;
using Core.Users;

namespace QuiApp.WebMVC.Controllers.Quiz.Models;

public class ResultDto
{
    public User User { get; set; } = null!;
    public UserProgress Progress { get; set; } = null!;
    public List<Question> Questions { get; set; } = null!;
}
