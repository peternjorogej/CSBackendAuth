
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Auth.Api.Models;
using Auth.Api.Services;
using Auth.Api.Utils;

namespace Auth.Api.Controllers;

public struct MyJwtInfo
{
    public const string Key      = "JsonWebTokenInfo.Key";
    public const string Issuer   = "JsonWebTokenInfo.Issuer";
    public const string Audience = "JsonWebTokenInfo.Audience";
}

public struct LoginCredentials
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public readonly bool IsValid => UserName.Length > 0 && Password.Length > 0;
}

public struct RegistrationCredentials
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Registration { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }

    public readonly bool IsValid => FirstName.Length > 0    &&
                                    LastName.Length  > 0    &&
                                    Registration.Length > 0 &&
                                    UserName.Length  > 0    &&
                                    Password.Length  > 0    &&
                                    PasswordConfirm.Length > 0;
}


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // private readonly ILogger<AuthController> m_Logger;

    // private readonly SignInManager<User> m_SignInManager;
    // private readonly UserManager<User> m_UserManager;
    
    public readonly UserDatabaseService m_UserDatabase;
    public readonly SqliteDatabaseService m_SqliteDatabase;
    
    public readonly JwtSecurityTokenHandler m_SecurityTokenHandler = new JwtSecurityTokenHandler(); 
    
    public AuthController(
        // SignInManager<User> signInManager,
        // UserManager<User> userManager,
        UserDatabaseService userDatabase,
        SqliteDatabaseService sqliteDatabase
        /*, ILogger<AuthController> logger */)
    {
        // m_Logger = logger;
        // m_SignInManager = signInManager;
        // m_UserManager = userManager;
        m_UserDatabase = userDatabase;
        m_SqliteDatabase = sqliteDatabase;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginCredentials credentials)
    {
        if (!credentials.IsValid)
        {
            return BadRequest();
        }

        User? user = m_UserDatabase.User.Where(u => u.UserName == credentials.UserName).FirstOrDefault();
        if (user is not null && Pswd.Verify(credentials.UserName, user.PasswordHash!, credentials.Password))
        {
            string token = GenerateJWT(user);
            return Ok(new { Message = "Log in successful", Token = token });
        }

        return Unauthorized(new{ Message = "Not logged in", Token = "" });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return new JsonResult(new{ Message = "Not logged out" });
    }

    [Authorize]
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        return new JsonResult(m_UserDatabase.User);
    }

    [Authorize]
    [HttpGet("users/search")]
    public IActionResult GetUserByUserName([FromQuery] string UserName)
    {
        User? user = m_UserDatabase.User.Where(u => u.UserName == UserName).FirstOrDefault();
        if (user is not null)
        {
            return new JsonResult(user!);
        }

        return new JsonResult(new { Message = $"User with username `{UserName}` not found" });
    }

    [HttpPost("register")]
    public IActionResult Register(RegistrationCredentials credentials)
    {
        if (!credentials.IsValid)
        {
            return BadRequest();
        }

        if (credentials.Password != credentials.PasswordConfirm)
        {
            return BadRequest(new { Message = "Passwords do not match" });
        }

        // Create and add the new client
        Client client = new Client
        {
            FirstName    = credentials.FirstName,
            LastName     = credentials.LastName,
            Registration = credentials.Registration,
            Email        = credentials.Email
        };
        m_SqliteDatabase.Clients.Add(client);
        m_SqliteDatabase.SaveChanges();

        // Create and add the new user
        User user = new User
        {
            ClientId = client.Id,
            UserName = credentials.UserName,
            PasswordHash = Pswd.Hash(credentials.UserName, credentials.Password), // m_PasswordHasher.HashPassword(credentials.UserName, credentials.Password),
            Email    = credentials.Email,
        };
        m_UserDatabase.User.Add(user);
        m_UserDatabase.SaveChanges();

        string token = GenerateJWT(user);
        return Ok(new { Message = "Registration successful", Token = token });
        
        /* IdentityResult result = await m_UserManager.CreateAsync(user, credentials.Password);
        if (result.Succeeded)
        {
            string token = GenerateJWT(user);
            return Ok(new { Message = "Registration successful", Token = token });
        }

        return BadRequest(new { Message = "Registration failed", result.Errors }); */
    }

    private string GenerateJWT(User user)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MyJwtInfo.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: MyJwtInfo.Issuer,
            audience: MyJwtInfo.Issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return m_SecurityTokenHandler.WriteToken(token);
    }
}

