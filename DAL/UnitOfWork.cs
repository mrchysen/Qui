using Core.Questions.QuestionServices;
using Core.Users.Services;

namespace DAL;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    public IQuestionRespository QuestionRespository { get; }
}

public class UnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Dispose() => _context.Dispose();
}
