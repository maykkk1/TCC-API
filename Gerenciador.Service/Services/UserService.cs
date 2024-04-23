using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    private readonly IEntityDtoMapper<User, CadastroDto> _cadastroMapper;

    public UserService(IUserRepository userRepository, IValidator<User> userValidator, IEntityDtoMapper<User, CadastroDto> cadastroMapper)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _cadastroMapper = cadastroMapper;
    }

    public async Task<User> Add(User obj)
    {
        var validationResult = await _userValidator.ValidateAsync(obj);
        if (validationResult.IsValid)
        {
            await _userRepository.Insert(obj);
            return obj;
        }
        else
        {
            // Tratar o erro de validação de alguma forma
            return null;
        }
    }

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }

    public async Task<IList<User>> Get()
    {
        return await _userRepository.Select();
    }

    public async Task<User> Update(User obj)
    {
        await _userRepository.Update(obj);
        return obj;
    }

    public async Task<ServiceResult<CadastroDto>> Cadastrar(CadastroDto user)
    {
        var obj = _cadastroMapper.DtoToEntity(user);
        
        try
        {
            await _userRepository.Insert(obj);
            return new ServiceResult<CadastroDto>() { Data = user };
        }
        catch (Exception ex)
        {
            // Tratar exceção de alguma forma
            return new ServiceResult<CadastroDto>() { ErrorMessage = ex.Message };
        }
    }

    public async Task<User> ValidateLogin(UserLoginDto user)
    {
        return await _userRepository.ValidateLogin(user);
    }

    public async Task<List<User>> GetOrientandosById(int orientadorId)
    {
        return await _userRepository.GetOrientandosById(orientadorId);
    }

    public async Task<UserDto> GetAlunoById(int id)
    {
        return await _userRepository.GetAlunoById(id);
    }

    // Remova ou implemente corretamente este método
    public Task<ServiceResult<T>> GetById<T>(int id)
    {
        throw new NotImplementedException();
    }
}