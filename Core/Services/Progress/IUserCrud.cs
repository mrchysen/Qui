using Core.Models;

namespace RazorPagesApp.Services.Progress;

public interface IUserCRUD
{
    public void SaveUser(User user);
    public List<User> GetUsers();
    public User GetUser(Guid id);
    public void DeleteUser(Guid id);
    public void DeleteAllUser();
}
