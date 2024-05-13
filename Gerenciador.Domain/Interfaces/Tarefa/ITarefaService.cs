using FluentValidation;

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Service.Common;

public interface ITarefaService 
{    
    Task<ServiceResult<TarefaDto>> Add(TarefaDto obj);

    Task<ServiceResult<int>> Delete(int tarefaId, int userId);

    Task<ServiceResult<IList<TarefaDto>>> Get();

    public Task<ServiceResult<TarefaDto>> GetById(int id);

    Task<ServiceResult<TarefaDto>> Update(TarefaDto obj);
    Task<ServiceResult<List<TarefaDto>>> getByUserId(int projetoId);
    Task<ServiceResult<List<TarefaDto>>> getByProjetctId(int projetoId);
    Task<ServiceResult<Tarefa>> InsertTarefaPrincipal(Tarefa tarefa);
    Task<ServiceResult<TarefaDto>> UpdateTarefaPrincipal(Tarefa tarefa, int userId);
}