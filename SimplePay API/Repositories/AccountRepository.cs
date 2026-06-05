using SimplePay_API.Context;
using SimplePay_API.Models;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public Account CreateAccount(Account account)
    {
        if(account == null) throw new ArgumentNullException(nameof(account));

        _context.Accounts.Add(account);
        _context.SaveChanges();

        return account;
        
    }

    public Account DeleteAccount(int id)
    {
        var accountToRemove = _context.Accounts.Find(id);

        if(id == null) throw new ArgumentNullException(nameof(accountToRemove));

        _context.Accounts.Remove(accountToRemove);
        _context.SaveChanges();

        return accountToRemove;

    }

    public Account GetAccountById(int id)
    {
        return _context.Accounts.FirstOrDefault(a => a.AccountId == id);
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _context.Accounts.ToList();
    }

}
