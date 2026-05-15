using Microsoft.AspNetCore.Mvc;
using SimplePay_API.Context;
using SimplePay_API.Models;
using System.Collections;

namespace SimplePay_API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

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
    [HttpGet("{id:int}")]//Aqui travamos a nossa a somente receber inteiros, se receber outra coisa e 400
    public ActionResult<User> GetUserById(int id)
    {
        if (id <= 0) return NotFound($"Id invalido");

        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return NotFound($"Usuario de id = {id} nao encontrado!");

        return Ok(user);
    }
}
