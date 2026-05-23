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
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
    {
        try
        {
            var listUsers = await _context.Users
            .AsNoTracking()
            .ToListAsync();

            if (listUsers == null) return NotFound("Usuarios nao encotrado...");

            return Ok(listUsers);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }

    }

    [HttpGet("{id:int}")]//Aqui travamos a nossa rota a somente receber inteiros, se receber outra coisa e 400
    public async Task<ActionResult<User>> GetUserByIdAsync(int id)
    {
        try
        {
            if (id <= 0) return NotFound($"Id = {id} invalido...");

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound($"Usuario de id = {id} nao encontrado!");

            return Ok(user);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = user.Id }, User);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUserAsync(int id, User user) 
    {
        //Aqui temos um exemplo de update completo, onde primeiro buscamos o usuario no banco,
        //depois fazemos o mapeamento manual dos campos e por fim atualizamos o usuario.

        try
        {
            if (id != user.Id) return BadRequest($"Id de usuarios divergentes...");

           
            var userInDB = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            //Mapeamento manual, poderia ser feito com o AutoMapper!
            userInDB.Name = user.Name;
            userInDB.Email = user.Email;

            _context.Users.Update(user); //Update efetuado em memória
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound($"Usuario de id = {id} nao encontrado...");

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
       
    }
}
