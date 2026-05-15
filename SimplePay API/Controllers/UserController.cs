using Microsoft.AspNetCore.Mvc;
using SimplePay_API.Context;
using SimplePay_API.Models;
using System.Collections;

namespace SimplePay_API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext? _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        var listUsers = _context.Users.ToList();
        
        if(listUsers == null) return NotFound("Usuarios nao encotrado...");

        return Ok(listUsers);

    }
}
