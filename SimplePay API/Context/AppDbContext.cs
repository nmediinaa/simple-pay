using Microsoft.EntityFrameworkCore;
using SimplePay_API.Models;

namespace SimplePay_API.Context;

public class AppDbContext : DbContext
{
    //Aqui passamos as opts de config que sera usada para config do DB, como a connString
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {
            
    }

    //Mapeanda <Entidades> => tabelas no banco
    public DbSet<Account> Accounts{ get; set; }
    public DbSet<User> Users{ get; set; }
}
