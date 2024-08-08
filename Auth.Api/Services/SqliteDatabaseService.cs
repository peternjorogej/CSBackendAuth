
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Auth.Api.Models;
using Auth.Api.Utils;

namespace Auth.Api.Services;

public class SqliteDatabaseService : DbContext
{
    // Concrete tables
    public required DbSet<Client> Clients { get; set; }

    private readonly ILogger<SqliteDatabaseService> m_Logger;

    public SqliteDatabaseService(DbContextOptions<SqliteDatabaseService> options, ILogger<SqliteDatabaseService> logger, IConfiguration configuration)
        : base(options)
    {
        m_Logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // SetupTableIndexesAndProperties(modelBuilder);
        // SetupTableRelationships(modelBuilder);

        m_Logger.LogInformation("Creating model: SqliteDatabaseService");
        SeedDatabase(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        m_Logger.LogInformation("Configuring model: SqliteDatabaseService");
        optionsBuilder.UseSqlite($"Data Source=Data/Auth.db");
        base.OnConfiguring(optionsBuilder);
    }
    
    private static void SeedDatabase(ModelBuilder modelBuilder)
    {
        // Create clients
        Client clientOne = new Client
        {
            Id           = 1,
            FirstName    = "Michael",
            LastName     = "Mathu",
            Registration = "BMIT-01/2023",
            Email        = "michaelmathu@gmail.com"
        };
        Client clientTwo = new Client
        {
            Id           = 2,
            FirstName    = "Peter",
            LastName     = "Njoroge",
            Registration = "BMIT-02/2023",
            Email        = "peternjoroge@gmail.com",
        };

        modelBuilder.Entity<Client>().HasData(clientOne, clientTwo);

        // Create respective users from those clients
        const string userNameOne = "michaelmathu";
        const string userNameTwo = "peternjoroge";

        modelBuilder.Entity<User>().HasData(
            new User
            {
                ClientId = clientOne.Id,
                UserName = userNameOne,
                Email    = clientOne.Email,
                PasswordHash = Pswd.Hash(userNameOne, "12345678"),
            },
            new User
            {
                ClientId = clientTwo.Id,
                UserName = userNameTwo,
                Email    = clientTwo.Email,
                PasswordHash = Pswd.Hash(userNameTwo, "87654321"),
            }
        );
    }

} 

