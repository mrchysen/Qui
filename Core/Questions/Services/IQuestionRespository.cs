using Core.Questions;
using System.Linq.Expressions;

namespace Core.Questions.QuestionServices;

public interface IQuestionRespository
{
    void SaveQuestions(List<Question> questions);
    Task<List<Question>> GetQuestions();
    void DeleteQuestion(Guid id);

    Task<List<Question>> GetByFilter(Expression<Func<Question, bool>> filter);
    Task AddQuestion(Question question);

    Task Update(Question question);
}
