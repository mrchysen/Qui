using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qui.Core.Models;

namespace Qui.DAL;

// TODO вынести ConnectionString
public class AppDbContext : DbContext
{
    private const string ConnectionString = @"Data Source=ApplicationDB.db;";
    private readonly List<Registration> _startRegistrations;

    public AppDbContext(IConfiguration configuration)
    {
        _startRegistrations = configuration.GetSection("AdminStartData").Get<List<Registration>>() ??
            throw new NullReferenceException("No section AdminStartData");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var registration in _startRegistrations)
            modelBuilder.Entity<Registration>().HasData(registration);
    }

    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }
}