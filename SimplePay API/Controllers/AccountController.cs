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
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsAsync()
    {
        try
        {
            var listAccounts = await _context.Accounts
            .Include(a => a.User)
            .AsNoTracking()
            .ToListAsync();

            if (listAccounts == null) return NotFound();

            return Ok(listAccounts);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetAccountByIdAsync(int id)
    {

        try
        {
            var account = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null) return NotFound($"Conta de id = {id} nao encontrada...");
            return Ok(account);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
        
    }


    [HttpPost]
    public async Task<ActionResult> CreateAccountAsync(Account account)
    {

        try
        {
            bool isAccountExist = _context.Accounts.Any(a => a.AccountId == account.AccountId);

            if (isAccountExist) return BadRequest($"A conta com o id = {account.AccountId} ja existe...");

            int userId = account.UserId;
            var isIdValid = _context.Users.Any(u => u.Id == userId);

            if (!isIdValid) return BadRequest($"O User Id = {userId} nao e valido...");


            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccountByIdAsync), new { id = account.AccountId }, account);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao processar sua requisicao...");
        }
       
    }

}
