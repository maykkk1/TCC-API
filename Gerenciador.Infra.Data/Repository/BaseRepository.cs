using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbContext _dbContext;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Insert(TEntity obj)
    {
        _dbContext.Set<TEntity>().Add(obj);
        _dbContext.SaveChanges();
    }

    public void Update(TEntity obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _dbContext.Set<TEntity>().Remove(Select(id));
        _dbContext.SaveChanges();
    }

    public IList<TEntity> Select()
    {
        return _dbContext.Set<TEntity>().ToList();
    }

    public TEntity Select(int id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }
}