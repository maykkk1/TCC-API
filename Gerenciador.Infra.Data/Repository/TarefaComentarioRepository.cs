using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces.TarefasComentario;

namespace Gerenciador.Infra.Data.Repository;

public class TarefaComentarioRepository : ITarefaComentarioRepository
{
    public Task Insert(TarefaComentario obj)
    {
        throw new NotImplementedException();
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