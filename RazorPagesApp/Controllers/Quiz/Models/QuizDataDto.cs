using Core.Questions;

namespace QuiApp.WebMVC.Controllers.Quiz.Models;

public class QuizDataDto
{
    public string QuestionPackName { get; set; }

    public List<Question> Questions { get; set; } = null!;
}
