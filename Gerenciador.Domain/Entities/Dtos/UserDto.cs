using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities.Dtos;

public class UserDto
{
    public string Name { get; set; }
    public TipoPessoaEnum Tipo { get; set; }
}