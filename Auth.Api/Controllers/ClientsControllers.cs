
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Auth.Api.Models;
using Auth.Api.Services;

namespace Auth.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    // private readonly ILogger<ClientsController> m_Logger;

    public readonly SqliteDatabaseService m_SqliteDatabase;
    
    public ClientsController(SqliteDatabaseService sqliteDatabase/* , ILogger<ClientsController> logger */)
    {
        // m_Logger = logger;
        m_SqliteDatabase = sqliteDatabase;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetClients()
    {
        return new JsonResult(m_SqliteDatabase.Clients);
    }

    [Authorize]
    [HttpGet("{Id}")]
    public IActionResult GetClientById(int Id)
    {
        Client? client = m_SqliteDatabase.Clients.Where(c => c.Id == Id).FirstOrDefault();
        if (client is not null)
        {
            return new JsonResult(client);
        }

        return new JsonResult(new { Message = $"Client with Id `{Id}` not found" });
    }
}

