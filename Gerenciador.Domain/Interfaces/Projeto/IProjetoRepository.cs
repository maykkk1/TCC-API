namespace Gerenciador.Domain.Interfaces.Projeto;

public interface IProjetoRepository : IBaseRepository<Entities.Projeto>
{
    Task AddUser(int userId, int projetoId);
    Task<List<Entities.Projeto>> GetByUserId(int userId);
    Task<Entities.Projeto> GetById(int projetoId);
    Task AddIntegrante(int projetoId, int integranteId);
    Task<bool> RelacionamentoExist(int projetoId, int integranteId);
    Task RemoverIntegrante(int projetoId, int integranteId);
}