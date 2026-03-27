using Qui.Core.Models;

namespace Qui.Core.Services.Progress;

public interface IUserRepository
{
    public void SaveUser(User user);
    public List<User> GetUsers();
    public User GetUser(Guid id);
    public void DeleteUser(Guid id);
    public void DeleteAllUser();
}
