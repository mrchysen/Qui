using Core.Questions;
using System.Linq.Expressions;

namespace Core.Questions.QuestionServices;

public interface IQuestionRespository
{
    Task<List<Question>> GetQuestions();
    Task<List<Question>> GetByFilter(Expression<Func<Question, bool>> filter);
    void AddQuestions(List<Question> questions);
    Task AddQuestion(Question question);
    Task Update(Question question);
    void DeleteQuestion(Guid id);
    Task DeleteAll();
}
