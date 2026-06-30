using System.Linq;
using System.Security.Principal;
using SimplePay_API.Context;
using SimplePay_API.Models;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
