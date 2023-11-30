using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public User Add<TValidator>(User obj) where TValidator : AbstractValidator<User>
    {
        Validate(obj, Activator.CreateInstance<TValidator>());
        _userRepository.Insert(obj);
        return obj;
    }

    public void Delete(int id)
    {
        _userRepository.Delete(id);
    }

    public IList<User> Get()
    {
        return _userRepository.Select();
    }

    public User GetById(int id)
    {
        return _userRepository.Select(id);
    }

    public User Update<TValidator>(User obj) where TValidator : AbstractValidator<User>
    {
        Validate(obj, Activator.CreateInstance<TValidator>());
        _userRepository.Update(obj);
        return obj;
    }
    
    private void Validate(User obj, AbstractValidator<User> validator)
    {
        if (obj == null)
            throw new Exception("Registros não detectados!");

        validator.ValidateAndThrow(obj);
    }
}