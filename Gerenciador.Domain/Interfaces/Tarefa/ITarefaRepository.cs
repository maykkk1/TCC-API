

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;

public interface ITarefaRepository : IBaseRepository<Tarefa>
{
    Task<List<Tarefa>> GetByUserId(int userId);
}