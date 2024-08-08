
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Models;

public class User : IdentityUser
{
    public required int ClientId { get; set; }
}

