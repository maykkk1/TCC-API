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

    public void Insert(Tarefa obj)
    {
        _dbContext.Set<Tarefa>().Add(obj);
        _dbContext.SaveChanges();
    }

    public void Update(Tarefa obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _dbContext.Set<Tarefa>().Remove(Select(id));
        _dbContext.SaveChanges();
    }

    public IList<Tarefa> Select()
    {
        return _dbContext.Set<Tarefa>().ToList();
    }

    public Tarefa Select(int id)
    {
        return _dbContext.Set<Tarefa>().Find(id);
    }
}