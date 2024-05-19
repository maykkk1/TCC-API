using FluentValidation;
using Gerenciador.Domain.Entities;

namespace Gerenciador.Service.Validators;

public class ProjetoValidator : AbstractValidator<Projeto>
{
    public ProjetoValidator()
    {
        RuleFor(c => c.Titulo)
            .NotEmpty().WithMessage("Titulo não informado.");
        RuleFor(c => c.Descricao)
            .NotEmpty().WithMessage("Descrição não informado.");
        RuleFor(c => c.OrientadorId)
            .NotEmpty().WithMessage("Id do responsável não informado.");
    }
}