using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IBaseRepository<User> baseRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Add<TValidator>(User obj) where TValidator : AbstractValidator<User>
    {
        Validate(obj, Activator.CreateInstance<TValidator>());
        await _userRepository.Insert(obj);
        return obj;
    }

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }

    public async Task<IList<User>> Get()
    {
        return await _userRepository.Select();
    }

    public async Task<User> GetById(int id)
    {
        return await _userRepository.Select(id);
    }

    public async Task<User> Update<TValidator>(User obj) where TValidator : AbstractValidator<User>
    {
        Validate(obj, Activator.CreateInstance<TValidator>());
        await _userRepository.Update(obj);
        return obj;
    }

    public async Task<User> ValidateLogin(UserLoginDto user)
    {
        return await _userRepository.ValidateLogin(user);
    }

    private void Validate(User obj, AbstractValidator<User> validator)
    {
        if (obj == null)
            throw new Exception("Registros não detectados!");

        validator.ValidateAndThrow(obj);
    }
}