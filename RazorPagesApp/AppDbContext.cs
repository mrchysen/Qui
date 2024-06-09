using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Authorization;
using RazorPagesApp.Services.Progress;
using RazorPagesApp.Services.Questions;

namespace EFLearning;

public class AppDbContext : DbContext, IQuestionsBD, IUserCRUD, IAdminRegistration
{
    private const string ConnectionString =
        @"Data Source=ApplicationDB.db;";
    private readonly List<Registration> stratRegistrations;

    public AppDbContext(IConfiguration AdminData)
    {
        //Database.EnsureDeleted();

        stratRegistrations = AdminData.GetSection("AdminStartData").Get<List<Registration>>();
        
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.
            UseSqlite(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var registration in stratRegistrations)
            modelBuilder.Entity<Registration>().HasData(registration);
    }

    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }

    #region IQuestionsBD
    public void AddQuestion(Question question)
    {
        Questions.Add(question);

        SaveChanges();
    }

    public void DeleteQuestion(Guid id)
    {
        var question = Questions.Find(id);

        if (question == null)
            return;

        Questions.Remove(question);

        SaveChanges();
    }

    public async void SaveQuestions(List<Question> questions)
    {
        Questions.ExecuteDelete();
        
        await Questions.AddRangeAsync(questions);

        await SaveChangesAsync();
    }

    public List<Question> GetQuestions()
    {
        return Questions.OrderBy(q => q.Order).ToList();
    }
    #endregion

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

        if(user == null)
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

    #region IAdminRegistration

    public bool IsAdmin(Registration registration)
    {
        return Registrations.ToList().Contains(registration);
    }

    #endregion
}
