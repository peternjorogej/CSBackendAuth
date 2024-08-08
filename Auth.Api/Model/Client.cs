
namespace Auth.Api.Models;

public class Client
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Registration { get; set; }
    public string? Email { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";
}


public class ClientDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Registration { get; set; }
    public string? Email { get; set; }

    public Client GetClient() => new Client
    {
        FirstName = FirstName,
        LastName  = LastName,
        Registration = Registration,
        Email     = Email
    };
}

