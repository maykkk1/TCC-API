using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> Add(TEntity obj);

    Task Delete(int id);

    Task<IList<TEntity>> Get();

    public Task<ServiceResult<T>> GetById<T>(int id);

    Task<TEntity> Update(TEntity obj);
}