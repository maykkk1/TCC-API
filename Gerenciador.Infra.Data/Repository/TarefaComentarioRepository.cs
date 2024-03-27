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

    public async Task Insert(TarefaComentario obj)
    {
        // obj.DataCriacao = DateTime.Now IMPLEMENTAR;
        _dbContext.Set<TarefaComentario>().Add(obj);
        await _dbContext.SaveChangesAsync();
    }

    public Task Update(TarefaComentario obj)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TarefaComentario>> Select()
    {
        throw new NotImplementedException();
    }

    public Task<TarefaComentario> Select(int id)
    {
        throw new NotImplementedException();
    }
}