using Core.Users;
using Core.Users.Services;
using DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace QuiApp.DAL.UserProgressServices;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
        => _appDbContext = appDbContext;

    public async Task AddUser(User user)
    {
        await _appDbContext.Users.AddAsync(user);

        _appDbContext.SaveChanges();
    }

    public async Task DeleteAllUser()
        => await _appDbContext.Users.ExecuteDeleteAsync();

    public async Task DeleteUser(Guid id)
    {
        var user = await _appDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return;
        }

        _appDbContext.Users.Remove(user);

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUser(Guid id)
        => await _appDbContext.Users.Include(x => x.Progress).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<User>> GetUsers()
        => await _appDbContext.Users.Include(x => x.Progress).ToListAsync();

    public async Task<List<User>> GetUsersByFilter(
        Expression<Func<User, bool>> filter)
        => await _appDbContext.Users.Where(filter).ToListAsync();
}
