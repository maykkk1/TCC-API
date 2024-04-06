namespace Gerenciador.Domain.Entities.Dtos;

public class TarefaComentarioDto
{
    public int Id { get; set; }
    public string Conteudo { get; set; }
    public int TarefaId { get; set; }
    public string AutorNome { get; set; }
    public int AutorId { get; set; }
    public DateTime DataComentario { get; set; }
}