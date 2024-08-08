
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Auth.Api.Models;
using Microsoft.AspNetCore.Hosting;

namespace Auth.Api.Services;

public class UserDatabaseService : DbContext
{
    public required DbSet<User> User { get; set; } // Find a way to change name to `Users`

    private readonly ILogger<UserDatabaseService> m_Logger;

    public UserDatabaseService(ILogger<UserDatabaseService> logger)
        : base()
    {
        m_Logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        m_Logger.LogInformation("Creating model: UserDatabaseService");
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        m_Logger.LogInformation("Configuring model: UserDatabaseService");
        optionsBuilder.UseSqlite($"Data Source=Data/Auth.db");
        base.OnConfiguring(optionsBuilder);
    }
}

