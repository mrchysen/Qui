using Qui.Core.Services.Progress;
using Qui.Core.Services.Questions;

namespace Qui.DAL;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    public IQuestionRespository QuestionRespository { get; }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository UserRepository => _context;
    public IQuestionRespository QuestionRespository => _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Dispose() => _context.Dispose();
}
