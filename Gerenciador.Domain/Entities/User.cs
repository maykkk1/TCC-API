using Gerenciador.Domain.Entities.Mappers;
using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public int Pontos { get; set; }
    public TipoPessoaEnum Tipo { get; set; }
    public int? OrientadorId { get; set; }
    public User? Orientador { get; set; }
    public string Telefone { get; set; }
    public string Password { get; set; }
    public int RankId { get; set; }
    public Ranks Rank { get; set; }
    public ICollection<Tarefa>? Tarefas { get; set; }
    public ICollection<Tarefa>? TarefasCriadas { get; set; }
    public ICollection<TarefaComentario>? Comentarios { get; set; }
    public ICollection<Atividade>? AtividadesCriadas { get; set; }
    public ICollection<AtividadePessoaRelacionamento>? AtividadesRelacionadas { get; set; }
    public ICollection<ProjetoPessoaRelacionamento>? ProjetosRelacionados { get; set; }
    public ICollection<Projeto>? Projetos { get; set; }
}