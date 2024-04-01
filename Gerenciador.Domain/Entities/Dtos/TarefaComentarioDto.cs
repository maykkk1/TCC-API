namespace Gerenciador.Domain.Entities.Dtos;

public class TarefaComentarioDto
{
    public int Id { get; set; }
    public string Conteudo { get; set; }
    public int TarefaId { get; set; }
}