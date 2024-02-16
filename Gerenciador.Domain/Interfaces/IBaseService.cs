using FluentValidation;
using Gerenciador.Domain.Entities;

namespace Gerenciador.Domain.Interfaces;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> Add(TEntity obj);

    Task Delete(int id);

    Task<IList<TEntity>> Get();

    Task<TEntity> GetById(int id);

    Task<TEntity> Update(TEntity obj);
}