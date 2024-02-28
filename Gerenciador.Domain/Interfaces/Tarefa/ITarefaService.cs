using FluentValidation;

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Service.Common;

public interface ITarefaService 
{    
    Task<ServiceResult<TarefaDto>> Add(TarefaDto obj);

    Task Delete(int id);

    Task<ServiceResult<IList<TarefaDto>>> Get();

    public Task<ServiceResult<TarefaDto>> GetById(int id);

    Task<ServiceResult<TarefaDto>> Update(TarefaDto obj);
    Task<ServiceResult<List<TarefaDto>>> getByUserId(int userId, bool isPrincipal);
    Task<ServiceResult<Tarefa>> InsertTarefaPrincipal(Tarefa tarefa);
    Task<ServiceResult<TarefaDto>> UpdateTarefaPrincipal(Tarefa tarefa, int userId);
}