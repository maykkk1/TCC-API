using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }
    public TipoPessoaEnum Tipo { get; set; }

    public string Password { get; set; }
}