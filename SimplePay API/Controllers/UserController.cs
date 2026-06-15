using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePay_API.Context;
using SimplePay_API.Models;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }


    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        try
        {
            var listUsers = _repository.GetAllUsers();
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

            var user = _repository.GetUserById(id);

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
            _repository.CreateUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateUser(int id, User user) 
    {
        //Aqui temos um exemplo de update completo, onde primeiro buscamos o usuario no banco,
        //depois fazemos o mapeamento manual dos campos e por fim atualizamos o usuario.

        try
        {
            if (id != user.Id) return BadRequest($"Id de usuarios divergentes...");

           
            _repository.UpdateUser(user);

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
            if(_repository.DeleteUser(id)) return NoContent();

            return NotFound();
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
       
    }
}
