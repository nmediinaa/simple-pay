using SimplePay_API.Context;
using SimplePay_API.Models;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private Repository<Account> _accountRepo;
    private Repository<User> _userRepo;

    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public Repository<Account> AccountRepository
    {
        get
        {
            return _accountRepo ?? new Repository<Account>(_context);
        }
    }


    public Repository<User> UserRepository
    {
        get
        {
            return _userRepo ?? new Repository<User>(_context);
        }
    }


    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
