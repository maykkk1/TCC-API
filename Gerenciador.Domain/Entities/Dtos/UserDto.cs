using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TipoPessoaEnum Tipo { get; set; }
    public int Pontos { get; set; }
    public RankDto Rank { get; set; }
}