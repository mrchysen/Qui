using Core.AdminAccess;
using Core.Questions;
using Core.Users;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
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
}
