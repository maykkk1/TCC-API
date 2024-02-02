using FluentValidation;
using Gerenciador.Domain.Entities;

namespace Gerenciador.Service.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public Tarefa Add<TValidator>(Tarefa obj) where TValidator : AbstractValidator<Tarefa>
    {
       _tarefaRepository.Insert(obj);
       return obj;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Tarefa> Get()
    {
        throw new NotImplementedException();
    }

    public Tarefa GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Tarefa Update<TValidator>(Tarefa obj) where TValidator : AbstractValidator<Tarefa>
    {
        throw new NotImplementedException();
    }
}