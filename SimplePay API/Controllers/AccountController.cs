using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePay_API.Context;
using SimplePay_API.Models;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public AccountController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Account>> GetAllAccounts()
    {
        try
        {
            var listAccounts = _uow.AccountRepository.GetAll();

            if (listAccounts == null) return NotFound();

            return Ok(listAccounts);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
        
    }

    [HttpGet("{id:int}")]
    public ActionResult GetAccountById(int id)
    {

        try
        {
            var account = _uow.AccountRepository.GetById(a => a.AccountId == id);

            if (account == null) return NotFound($"Conta de id = {id} nao encontrada...");

            return Ok(account);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }


    [HttpPost]
    public ActionResult CreateAccount(Account account)
    {

        try
        {
           _uow.AccountRepository.Create(account);

            return CreatedAtAction(nameof(GetAccountById), new { id = account.AccountId }, account);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
       
    }

}
