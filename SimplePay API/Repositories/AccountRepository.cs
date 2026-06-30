using SimplePay_API.Context;
using SimplePay_API.Models;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext context) : base(context)
    {
    }
}
