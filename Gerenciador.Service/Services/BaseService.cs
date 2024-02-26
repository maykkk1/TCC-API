using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _baseRepository;

    public BaseService(IBaseRepository<TEntity> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<TEntity> Add(TEntity obj)
    {
        await _baseRepository.Insert(obj);
        return obj;
    }

    public async Task Delete(int id) => await _baseRepository.Delete(id);

    public async Task<IList<TEntity>> Get() => await _baseRepository.Select();
    public Task<ServiceResult<T>> GetById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> GetById(int id) => await _baseRepository.Select(id);

    public async Task<TEntity> Update(TEntity obj)
    {
        await _baseRepository.Update(obj);
        return obj;
    }
}