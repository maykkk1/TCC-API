using FluentValidation;

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Service.Common;

public interface ITarefaService : IBaseService<Tarefa>
{
    Task<List<TarefaDto>> getByUserId(int userId, bool isPrincipal);
    Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa);
    Task<ServiceResult<TarefaDto>> UpdateTarefaPrincipal(Tarefa tarefa, int userId);
}