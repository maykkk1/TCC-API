using FluentValidation;
using Gerenciador.Domain.Entities;

namespace Gerenciador.Domain.Interfaces;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

    Task Delete(int id);

    Task<IList<TEntity>> Get();

    Task<TEntity> GetById(int id);

    Task<TEntity> Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
}