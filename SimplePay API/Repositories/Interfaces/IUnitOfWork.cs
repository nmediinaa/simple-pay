using SimplePay_API.Models;

namespace SimplePay_API.Repositories.Interfaces;

public interface IUnitOfWork
{
    Repository<Account> AccountRepository { get; }

    Repository<User> UserRepository { get; }

    void Commit();

}
