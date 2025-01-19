using Core.Questions;
using Core.Questions.QuestionServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.QuestionsServices;

public class QuestionsRepository : IQuestionRespository
{
    private readonly AppDbContext _appDbContext;

    public QuestionsRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddQuestion(Question question)
    {
        await _appDbContext.Questions.AddAsync(question);

        await _appDbContext.SaveChangesAsync();
    }

    public void DeleteQuestion(Guid id)
    {
        var question = _appDbContext.Questions.Find(id);

        if (question == null)
            return;

        _appDbContext.Questions.Remove(question);

        _appDbContext.SaveChanges();
    }

    public async void SaveQuestions(List<Question> questions)
    {
        _appDbContext.Questions.ExecuteDelete();

        await _appDbContext.Questions.AddRangeAsync(questions);

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<Question>> GetQuestions() 
        => await _appDbContext.Questions.OrderBy(q => q.Order).ToListAsync();

    public async Task<List<Question>> GetByFilter(Expression<Func<Question, bool>> filter) 
        => await _appDbContext.Questions.Where(filter).ToListAsync();

    public async Task Update(Question question)
    {
        _appDbContext.Update(question);

        await _appDbContext.SaveChangesAsync();
    }
}
