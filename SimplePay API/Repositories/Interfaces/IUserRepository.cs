using SimplePay_API.Models;

namespace SimplePay_API.Repositories.Interfaces;

public interface IUserRepository
{

     IEnumerable<User> GetAllUsers();
     User GetUserById(int id);
     User CreateUser(User user);
     bool UpdateUser(User user);
     bool DeleteUser(User user);

}
