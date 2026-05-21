using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePay_API.Context;
using SimplePay_API.Models;

namespace SimplePay_API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Account>> GetAllAccounts()
    {
        try
        {
            var listAccounts = _context.Accounts
            .Include(a => a.User)
            .AsNoTracking()
            .ToList();

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
            var account = _context.Accounts
            .AsNoTracking()
            .FirstOrDefault(a => a.AccountId == id);

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
            bool isAccountExist = _context.Accounts.Any(a => a.AccountId == account.AccountId);

            if (isAccountExist) return BadRequest($"A conta com o id = {account.AccountId} ja existe...");

            int userId = account.UserId;
            var isIdValid = _context.Users.Any(u => u.Id == userId);

            if (!isIdValid) return BadRequest($"O User Id = {userId} nao e valido...");


            _context.Accounts.Add(account);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAccountById), new { id = account.AccountId }, account);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
       
    }

}
