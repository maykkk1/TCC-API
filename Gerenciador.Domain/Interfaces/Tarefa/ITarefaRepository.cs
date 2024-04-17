

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

public interface ITarefaRepository : IBaseRepository<Tarefa>
{
    Task<List<TarefaDto>> GetByUserId(int userId, bool isPrincipal);
    Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa);
}