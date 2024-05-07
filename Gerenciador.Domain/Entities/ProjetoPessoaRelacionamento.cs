namespace Gerenciador.Domain.Entities;

public class ProjetoPessoaRelacionamento : BaseEntity
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public int ProjetoId { get; set; }

    public User Pessoa { get; set; }
    public Projeto Projeto { get; set; }
}