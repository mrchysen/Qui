using System.Linq.Expressions;

namespace Core.Users.Services;

public interface IUserRepository
{
    public Task AddUser(User user);
    public Task<List<User>> GetUsers();
    public Task<List<User>> GetUsersByFilter(Expression<Func<User,bool>> filter);
    public Task<User?> GetUser(Guid id);
    public Task DeleteUser(Guid id);
    public Task DeleteAllUser();
}