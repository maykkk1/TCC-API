using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class Tarefa : BaseEntity
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public SituacaoTarefaEnum Situacao  { get; set; }
    public User Pessoa { get; set; }
    public int IdPessoa { get; set; }
}