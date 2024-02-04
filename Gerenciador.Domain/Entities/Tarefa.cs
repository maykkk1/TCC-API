using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class Tarefa : BaseEntity
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public SituacaoTarefaEnum Situacao  { get; set; }
    public TipoTarefa Tipo { get; set; }
    public User? Pessoa { get; set; }
    public int IdPessoa { get; set; }
}