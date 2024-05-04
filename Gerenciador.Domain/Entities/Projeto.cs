namespace Gerenciador.Domain.Entities;

public class Projeto : BaseEntity
{
    public string Descricao { get; set; }
    public int OrientadorId { get; set; }
    public User Orientador { get; set; }
    public ICollection<Tarefa>? Tarefas { get; set; }
    public DateTime DataCriacao { get; set; }
}