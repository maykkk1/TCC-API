using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }
    public TipoPessoaEnum Tipo { get; set; }
    public int? OrientadorId { get; set; }
    public User? Orientador { get; set; }
    public string Password { get; set; }
    public ICollection<Tarefa>? Tarefas { get; set; }
    public ICollection<Tarefa>? TarefasCriadas { get; set; }
    public ICollection<TarefaComentario>? Comentarios { get; set; }
}