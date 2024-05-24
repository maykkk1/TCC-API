namespace Gerenciador.Domain.Entities;

public class Projeto : BaseEntity
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int OrientadorId { get; set; }
    public User Orientador { get; set; }
    public ICollection<Tarefa>? Tarefas { get; set; }
    public ICollection<Atividade>? Atividades { get; set; }
    public ICollection<ProjetoPessoaRelacionamento>? PessoasRelacionadas { get; set; }
    public DateTime DataCriacao { get; set; }
}