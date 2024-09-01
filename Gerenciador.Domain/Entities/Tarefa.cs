using System.ComponentModel.DataAnnotations.Schema;
using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class Tarefa : BaseEntity
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public SituacaoTarefaEnum Situacao  { get; set; }
    public DificuldadeTarefaEnum  Dificuldade  { get; set; }
    public TipoTarefa Tipo { get; set; }
    public User? Pessoa { get; set; }
    public User? CreatedBy { get; set; }
    public Projeto? Projeto { get; set; }
    public int CreatedById { get; set; }
    public int? PessoaId { get; set; }
    public int? ProjetoId { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataFinal { get; set; }
    public DateTime? DataCompleta { get; set; }
    public ICollection<TarefaComentario>? Comentarios { get; set; }
    public ICollection<Atividade>? Atividades { get; set; }
}