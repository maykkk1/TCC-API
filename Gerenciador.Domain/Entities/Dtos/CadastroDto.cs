using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities.Dtos;

public class CadastroDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Sobrenome { get; set; }
    public TipoPessoaEnum Tipo { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Confirm { get; set; }
}