namespace Gerenciador.Domain.Interfaces.Projeto;

public interface IProjetoRepository : IBaseRepository<Entities.Projeto>
{
    Task AddUser(int userId, int projetoId);
    Task<List<Entities.Projeto>> GetByUserId(int userId);
}