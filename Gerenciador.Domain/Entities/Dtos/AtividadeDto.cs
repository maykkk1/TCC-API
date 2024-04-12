using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities.Dtos;

public class AtividadeDto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public DateTime? DataAtividade { get; set; }
    public TipoAtividadeEnum Tipo { get; set; }
    public string Responsavel { get; set; }
    public string TarefaTitulo { get; set; }
    public int PessoaId { get; set; }
    public int TarefaId { get; set; }
    public SituacaoTarefaEnum NovaSituacaoTarefa { get; set; }
}