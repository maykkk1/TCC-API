using FluentValidation;

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

public interface ITarefaService : IBaseService<Tarefa>
{
    Task<List<TarefaDto>> getByUserId(int userId);
    Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa);
}