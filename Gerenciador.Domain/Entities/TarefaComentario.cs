namespace Gerenciador.Domain.Entities;

public class TarefaComentario : BaseEntity
{
    public string Conteudo { get; set; }
    public Tarefa? Tarefa { get; set; }
    public int TarefaId { get; set; }
}