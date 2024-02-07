using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;

namespace Gerenciador.Service.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<Tarefa> Add<TValidator>(Tarefa obj) where TValidator : AbstractValidator<Tarefa>
    {
       await _tarefaRepository.Insert(obj);
       return obj;
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Tarefa>> Get()
    {
        return await _tarefaRepository.Select();
    }

    public Task<Tarefa> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Tarefa> Update<TValidator>(Tarefa obj) where TValidator : AbstractValidator<Tarefa>
    {
        await _tarefaRepository.Update(obj);
        return obj;
    }

    public async Task<List<TarefaDto>> getByUserId(int userId)
    {
        return await _tarefaRepository.GetByUserId(userId);
    }

    public async Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa)
    {
        await _tarefaRepository.InsertTarefaPrincipal(tarefa);
        return tarefa;
    }
}