using Microsoft.EntityFrameworkCore;
using Qui.Core.Models;
using Qui.Core.Services.Questions;

namespace Qui.DAL.Repositories;

// Todo добавить логирование
// Todo добавить обработку cancelationToken в каждый ассинхронный метод
public class QuestionRespository(AppDbContext dbContext) : IQuestionRespository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task AddQuestion(Question question)
    {
        await _dbContext.Questions.AddAsync(question);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteQuestion(Guid id)
    {
        var question = _dbContext.Questions.Find(id);

        if (question == null)
            return;

        _dbContext.Questions.Remove(question);

        _dbContext.SaveChanges();
    }

    public async Task SaveQuestions(List<Question> questions)
    {
        // Тут поменялась локига, почему-то удаляю все вопросы и добавляю новые
        await _dbContext.Questions.AddRangeAsync(questions);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Question>> GetQuestions()
    {
        return await _dbContext.Questions
            .OrderBy(q => q.Order)
            .ToListAsync();
    }
}
