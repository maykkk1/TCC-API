namespace Gerenciador.Domain.Entities;

public class Conquista : BaseEntity
{
    public string Descricao { get; set; }
    public string Titulo { get; set; }
    
    public ICollection<ConquistaPessoaRelacionamento>? PessoasRelacionadas { get; set; }
}