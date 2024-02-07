using FluentValidation;

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;

public interface ITarefaService : IBaseService<Tarefa>
{
    Task<List<Tarefa>> getByUserId(int userId);
    Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa);
}