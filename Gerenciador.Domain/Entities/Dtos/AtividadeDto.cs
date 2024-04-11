namespace Gerenciador.Domain.Entities.Dtos;

public class AtividadeDto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public DateTime? DataAtividade { get; set; }
    public int PessoaId { get; set; }
    public User User { get; set; }
    public int TarefaId { get; set; }
    public Tarefa Tarefa { get; set; }
}