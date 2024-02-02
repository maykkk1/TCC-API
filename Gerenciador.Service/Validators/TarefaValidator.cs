using FluentValidation;
using Gerenciador.Domain.Entities;

namespace Gerenciador.Service.Validators;

public class TarefaValidator :  AbstractValidator<Tarefa>
{
    public TarefaValidator()
    {
    }
}