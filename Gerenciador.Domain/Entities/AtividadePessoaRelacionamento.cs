namespace Gerenciador.Domain.Entities;

public class AtividadePessoaRelacionamento
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public int AtividadeId { get; set; }

    public User Pessoa { get; set; }
    public Atividade Atividade { get; set; }
}