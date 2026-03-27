using Microsoft.EntityFrameworkCore;
using Qui.Core.Models;
using Qui.Core.Services.Progress;

namespace Qui.DAL.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public void SaveUser(User user)
    {
        _context.Users.Add(user);
        Console.WriteLine(user.Id);
        _context.SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _context.Users.Include(user => user.Progress).ToList();
    }

    public User GetUser(Guid id)
    {
        var user = _context.Users
            .Include(user => user.Progress)
            .Where(user => user.Id.Equals(id))
            .FirstOrDefault();

        if (user == null)
        {
            return new User()
            {
                Name = "NONAME"
            };
        }

        return user;
    }

    public void DeleteUser(Guid id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
            return;

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public async void DeleteAllUser()
    {
        await _context.Users.ExecuteDeleteAsync();
        _context.SaveChanges();
    }
}