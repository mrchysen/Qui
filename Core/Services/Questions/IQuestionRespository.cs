using Core.Models;

namespace Core.Services.Questions;

public interface IQuestionRespository
{
    public void SaveQuestions(List<Question> questions);
    public List<Question> GetQuestions();
    public void DeleteQuestion(Guid id);
    public void AddQuestion(Question question);
}
