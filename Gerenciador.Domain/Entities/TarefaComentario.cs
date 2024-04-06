namespace Gerenciador.Domain.Entities;

public class TarefaComentario : BaseEntity
{
    public string Conteudo { get; set; }
    public Tarefa? Tarefa { get; set; }
    public int TarefaId { get; set; }
    public User? Autor { get; set; }
    public int AutorId { get; set; }
    public string AutorNome { get; set; }
    public DateTime DataComentario { get; set; }
}