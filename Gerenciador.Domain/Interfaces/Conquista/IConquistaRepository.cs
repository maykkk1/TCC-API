using Gerenciador.Domain.Entities.Dtos;

namespace Gerenciador.Domain.Interfaces.Conquista;

public interface IConquistaRepository : IBaseRepository<Entities.Conquista>
{
    Task<List<ConquistaDto>> GetConquistasByUserId(int userId);
    Task InsertRelacionamento(int userId, int conquistaId);
    Task VerificarConquistaRank(int userId, int rankId);
}