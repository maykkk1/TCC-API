namespace Gerenciador.Domain.Entities.Dtos;

public class TarefaComentarioDto
{
    public int Id { get; set; }
    public string Conteudo { get; set; }
    public Tarefa Tarefa { get; set; }
    public int IdTarefa { get; set; }
}