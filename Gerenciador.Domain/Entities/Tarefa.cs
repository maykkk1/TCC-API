using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class Tarefa
{
    public string Id { get; set; }
    public string Descricao { get; set; }
    public SituacaoTarefaEnum Situacao  { get; set; }
    // chave estrangeira
    public int IdPessoa { get; set; }
}