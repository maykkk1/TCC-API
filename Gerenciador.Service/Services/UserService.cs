using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;

    public UserService(IUserRepository userRepository, IBaseRepository<User> baseRepository, IValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }
    public async Task<User> Add(User obj)
    {
        var validate = _userValidator.ValidateAsync(obj).Result;
        if (validate.IsValid)
        {
            return obj;
        }

        return null;
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

    public async Task<User> Update(User obj)
    {
        await _userRepository.Update(obj);
        return obj;
    }

    public async Task<User> ValidateLogin(UserLoginDto user)
    {
        return await _userRepository.ValidateLogin(user);
    }

    public async Task<List<User>> GetOrientandosById(int orientadorId)
    {
        return await _userRepository.GetOrientandosById(orientadorId);
    }

    private void Validate(User obj, AbstractValidator<User> validator)
    {
        if (obj == null)
            throw new Exception("Registros não detectados!");

        validator.ValidateAndThrow(obj);
    }
}