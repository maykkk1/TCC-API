using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities.Dtos;

public class RankDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public TipoPessoaEnum Tipo { get; set; }
    public int PontosMinimos { get; set; }
    public int PontosMaximos { get; set; }
    public ICollection<User>? Users { get; set; }
}