using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePay_API.Context;
using SimplePay_API.Models;

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
        var listUsers = _context.Users
            .AsNoTracking()
            .ToList();
        
        if(listUsers == null) return NotFound("Usuarios nao encotrado...");

        return Ok(listUsers);

    }

    [HttpGet("{id:int}")]//Aqui travamos a nossa rota a somente receber inteiros, se receber outra coisa e 400
    public ActionResult<User> GetUserById(int id)
    {
        if (id <= 0) return NotFound($"Id invalido");

        var user = _context.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.Id == id);

        if (user == null) return NotFound($"Usuario de id = {id} nao encontrado!");

        return Ok(user);
    }
    
    [HttpPost]
    public ActionResult CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, User);
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateUser(int id, User user) 
    {
        if (id != user.Id) return BadRequest();
       
        _context.Users.Update(user);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return NotFound();

        _context.Remove(user);
        _context.SaveChanges();

        return Ok(user);
    }
}
