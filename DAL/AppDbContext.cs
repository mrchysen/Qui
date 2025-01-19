using Core.AdminAccess;
using Core.Questions;
using Core.Questions.QuestionServices;
using Core.Users;
using Core.Users.Services;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext, IUserRepository
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<AdminRegistration> Registrations { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminRegistration>().HasData(
                new AdminRegistration { Id = Guid.NewGuid(), Login = "Admond", Password = "Price" });
    }

    #region IUserCRUD
    public void SaveUser(User user)
    {
        Users.Add(user);

        Console.WriteLine(user.Id);

        SaveChanges();
    }

    public List<User> GetUsers()
    {
        return Users.Include(user => user.Progress).ToList();
    }
    public User GetUser(Guid id)
    {
        var user = Users.Include(user => user.Progress).Where(user => user.Id.Equals(id)).First();

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
        var user = Users.Find(id);

        if (user == null)
            return;

        Users.Remove(user);

        SaveChanges();
    }
    public async void DeleteAllUser()
    {
        await Users.ExecuteDeleteAsync();

        SaveChanges();
    }
    #endregion
}
