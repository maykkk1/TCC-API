using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities.Dtos;

public class TarefaDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public SituacaoTarefaEnum Situacao  { get; set; }
    public TipoTarefa Tipo { get; set; }
    public UserDto Pessoa { get; set; }
    public UserDto CreatedBy { get; set; }
    public int CreatedById { get; set; }
    public int IdPessoa { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataFinal { get; set; }
    public int? IdTarefaRelacionada { get; set; }
}