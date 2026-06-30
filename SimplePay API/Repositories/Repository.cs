using System.Linq.Expressions;
using SimplePay_API.Context;
using SimplePay_API.Repositories.Interfaces;

namespace SimplePay_API.Repositories;

public class Repository<T> : IRepository<T> where T : class //Proteção contra a herança de classes que não sejam do tipo class
{
    protected readonly AppDbContext _context; //Aqui todas classes derivadas terão acesso ao contexto do banco de dados

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList(); //Set é um método do DbContext que retorna um DbSet especifico.
    }

    public T? GetById(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
        return entity;
    }
}
