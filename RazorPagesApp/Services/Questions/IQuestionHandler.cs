using RazorPagesApp.Models;
namespace RazorPagesApp.Services.Questions;
public interface IQuestionHandler
{
    public int Count { get; }
    public Question? this[int index] { get; }
    public void ChangeQuestion(int index, Question question);
    public void CreateQuestion(Question question);
    public void DeleteQuestion(int index);
    public void DeleteAllQuestions();
}
