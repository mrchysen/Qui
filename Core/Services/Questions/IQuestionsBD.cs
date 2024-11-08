using Core.Models;

namespace Core.Services.Questions;

public interface IQuestionsBD
{
    public void SaveQuestions(List<Question> questions);
    public List<Question> GetQuestions();
    public void DeleteQuestion(Guid id);
    public void AddQuestion(Question question);
}
