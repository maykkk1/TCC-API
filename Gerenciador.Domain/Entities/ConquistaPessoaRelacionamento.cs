namespace Gerenciador.Domain.Entities;

public class ConquistaPessoaRelacionamento
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public int ConquistaId { get; set; }

    public User Pessoa { get; set; }
    public Conquista Conquista { get; set; }
}