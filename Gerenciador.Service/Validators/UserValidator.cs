using FluentValidation;
using Gerenciador.Domain.Entities;

namespace Gerenciador.Service.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Nome não informado.")
            .NotNull().WithMessage("Nome não informado.");
        
        RuleFor(c => c.Sobrenome)
            .NotEmpty().WithMessage("Sobrenome não informado.")
            .NotNull().WithMessage("Sobrenome não informado.");
        
        RuleFor(c => c.Telefone)
            .NotEmpty().WithMessage("Telefone não informado.")
            .NotNull().WithMessage("Telefone não informado.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email não informado.")
            .NotNull().WithMessage("Email não informado.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Senha não informada.")
            .NotNull().WithMessage("Senha não informada.");
    }
}