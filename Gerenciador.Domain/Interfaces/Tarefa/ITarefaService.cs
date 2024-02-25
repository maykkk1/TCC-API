using FluentValidation;

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Service.Common;

public interface ITarefaService 
{    Task<TarefaDto> Add(TarefaDto obj);

    Task Delete(int id);

    Task<IList<TarefaDto>> Get();

    public Task<ServiceResult<TarefaDto>> GetById<T>(int id);

    Task<TarefaDto> Update(TarefaDto obj);
    Task<List<TarefaDto>> getByUserId(int userId, bool isPrincipal);
    Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa);
    Task<ServiceResult<TarefaDto>> UpdateTarefaPrincipal(Tarefa tarefa, int userId);
}