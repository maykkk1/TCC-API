using Gerenciador.Domain.Entities;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class TarefaRepository : ITarefaRepository
{
    protected readonly GerenciadorContext _dbContext;

    public TarefaRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Insert(Tarefa obj)
    {
        obj.DataCriacao = DateTime.Now;
        _dbContext.Set<Tarefa>().Add(obj);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Tarefa obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<Tarefa>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<Tarefa>> Select()
    {
        return await _dbContext.Set<Tarefa>().ToListAsync();
    }

    public async Task<Tarefa> Select(int id)
    {
        return await _dbContext.Set<Tarefa>().FindAsync(id);
    }

    public async Task<List<Tarefa>> GetByUserId(int userId)
    {
        return await _dbContext.Set<Tarefa>().Where(t => t.IdPessoa == userId).ToListAsync();
    }

    public async Task InsertTarefaPrincipal(Tarefa tarefa)
    {
        tarefa.DataCriacao = DateTime.Now;
        _dbContext.Set<Tarefa>().Add(tarefa);
        await _dbContext.SaveChangesAsync();
    }
}