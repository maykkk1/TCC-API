namespace Gerenciador.Domain.Entities.Dtos;

public class ProjetoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public int OrientadorId { get; set; }
    public DateTime DataCriacao { get; set; }
}