using Core.Users;

namespace Core.Users.Services;

public interface IUserRepository
{
    public void SaveUser(User user);
    public List<User> GetUsers();
    public User GetUser(Guid id);
    public void DeleteUser(Guid id);
    public void DeleteAllUser();
}
