using System.Linq;
using System.Security.Principal;
using SimplePay_API.Context;
using SimplePay_API.Models;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Repositories;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User CreateUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public bool DeleteUser(User user)
    {
        var userToDelete = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();

            return true;
        }

        return false;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUserById(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if(user == null) throw new ArgumentNullException(nameof(user));

        return user;
    }

    public bool UpdateUser(User user)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (userToUpdate == null) throw new ArgumentNullException(nameof(userToUpdate));

        if(_context.Users.Any(u => u.Id == user.Id))
        {
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;

            _context.Users.Update(userToUpdate);
            _context.SaveChanges();

            return true;
        }

        return false;
        
    }
}
