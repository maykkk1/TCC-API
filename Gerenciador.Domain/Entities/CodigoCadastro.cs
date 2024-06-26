using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Entities;

public class CodigoCadastro
{
    public int Id { get; set; }
    public int? Codigo { get; set; }
    public int? OrientadorId { get; set; }
    public Status Status { get; set; }
    public DateTime? DataExpiracao { get; set; }
}