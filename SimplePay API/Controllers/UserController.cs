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
        try
        {
            var listUsers = _context.Users
            .AsNoTracking()
            .ToList();

            if (listUsers == null) return NotFound("Usuarios nao encotrado...");

            return Ok(listUsers);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }

    }

    [HttpGet("{id:int}")]//Aqui travamos a nossa rota a somente receber inteiros, se receber outra coisa e 400
    public ActionResult<User> GetUserById(int id)
    {
        try
        {
            if (id <= 0) return NotFound($"Id = {id} invalido...");

            var user = _context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound($"Usuario de id = {id} nao encontrado!");

            return Ok(user);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }
    
    [HttpPost]
    public ActionResult CreateUser(User user)
    {
        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, User);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateUser(int id, User user) 
    {
        try
        {
            if (id != user.Id) return BadRequest($"Id de usuarios divergentes...");

            _context.Users.Update(user);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteUser(int id)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound($"Usuario de id = {id} nao encontrado...");

            _context.Remove(user);
            _context.SaveChanges();

            return Ok(user);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
       
    }
}
