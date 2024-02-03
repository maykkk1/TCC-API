using Gerenciador.Domain.Entities;

namespace Gerenciador.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task Insert(TEntity obj);

    Task Update(TEntity obj);

    Task Delete(int id);

    Task<IList<TEntity>> Select();

    Task<TEntity> Select(int id);
}