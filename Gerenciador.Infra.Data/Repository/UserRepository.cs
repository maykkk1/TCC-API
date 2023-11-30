using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class UserRepository : IUserRepository
{
    protected readonly GerenciadorContext _dbContext;

    public UserRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Insert(User obj)
    {
        _dbContext.Set<User>().Add(obj);
        _dbContext.SaveChanges();
    }

    public void Update(User obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _dbContext.Set<User>().Remove(Select(id));
        _dbContext.SaveChanges();
    }

    public IList<User> Select()
    {
        return _dbContext.Set<User>().ToList();
    }

    public User Select(int id)
    {
        return _dbContext.Set<User>().Find(id);
    }
}