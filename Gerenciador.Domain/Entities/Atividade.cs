using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class Atividade : BaseEntity
{
    public string Descricao { get; set; }
    public DateTime? DataAtividade { get; set; }
    public TipoAtividadeEnum Tipo { get; set; }
    public int PessoaId { get; set; }
    public User Pessoa { get; set; }
    public int TarefaId { get; set; }
    public Tarefa Tarefa { get; set; }
    public SituacaoTarefaEnum NovaSituacaoTarefa { get; set; }
    public ICollection<AtividadePessoaRelacionamento>? PessoasRelacionadas { get; set; }
}