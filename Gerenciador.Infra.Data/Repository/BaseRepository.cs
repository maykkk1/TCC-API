using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly GerenciadorContext _dbContext;

    public BaseRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> Insert(TEntity obj)
    {
        _dbContext.Set<TEntity>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public async Task Update(TEntity obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<TEntity>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<TEntity>> Select()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> Select(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }
}