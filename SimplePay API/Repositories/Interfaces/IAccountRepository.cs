using SimplePay_API.Models;

namespace SimplePay_API.Repositories.Interfaces;

public interface IAccountRepository
{

    IEnumerable<Account> GetAllAccounts();
    Account GetAccountById(int id);
    Account CreateAccount(Account account);
    Account DeleteAccount(int id);

}
