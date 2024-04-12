using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces.Atividade;

public interface IAtividadeRepository : IBaseRepository<Entities.Atividade>
{
    Task<List<Entities.Atividade>> GetByUserId(int userId);
}