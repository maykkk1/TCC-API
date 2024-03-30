using System.ComponentModel.DataAnnotations.Schema;
using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class Tarefa : BaseEntity
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public SituacaoTarefaEnum Situacao  { get; set; }
    public TipoTarefa Tipo { get; set; }
    public User? Pessoa { get; set; }
    public User? CreatedBy { get; set; }
    public int CreatedById { get; set; }
    public int IdPessoa { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataFinal { get; set; }
    public ICollection<TarefaComentario>? Comentarios { get; set; }
}