using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities.Dtos;

public class IntegranteDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
}