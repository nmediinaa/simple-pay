using SimplePay_API.Models;

namespace SimplePay_API.Repositories.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    //Não precisamos de métodos específicos, pois a interface genérica IRepository já fornece os métodos CRUD básicos para a entidade Account.
}
