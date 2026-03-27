using Qui.Core.Models;

namespace Qui.Core.Services.Questions;

public interface IQuestionRespository
{
    public Task SaveQuestions(List<Question> questions);
    public Task<List<Question>> GetQuestions();
    public Task DeleteQuestion(Guid id);
    public Task AddQuestion(Question question);
}
