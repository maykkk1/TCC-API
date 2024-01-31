using FluentValidation;

using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;

public class ITarefaService : IBaseService<Tarefa>
{
    public Tarefa Add<TValidator>(Tarefa obj) where TValidator : AbstractValidator<Tarefa>
    {
        throw new NotImplementedException();
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