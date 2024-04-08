using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces.TarefasComentario;
using Gerenciador.Infra.Data.Context;

namespace Gerenciador.Infra.Data.Repository;

public class TarefaComentarioRepository : ITarefaComentarioRepository
{
    protected readonly GerenciadorContext _dbContext;

    public TarefaComentarioRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TarefaComentario> Insert(TarefaComentario obj)
    {
        obj.DataComentario = DateTime.Now;
        _dbContext.Set<TarefaComentario>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public Task Update(TarefaComentario obj)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<TarefaComentario>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public Task<IList<TarefaComentario>> Select()
    {
        throw new NotImplementedException();
    }

    public async Task<TarefaComentario> Select(int id)
    {
        return await _dbContext.Set<TarefaComentario>().FindAsync(id);
    }
}