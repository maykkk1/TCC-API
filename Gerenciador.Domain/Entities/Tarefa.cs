namespace Gerenciador.Domain.Entities;

public class Tarefa
{
    public string Id { get; set; }
    public string Descricao { get; set; }
    // criar um enum aqui
    public bool Situacao  { get; set; }
    // chave estrangeira
    public int IdPessoa { get; set; }
}