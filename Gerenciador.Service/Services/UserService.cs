using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Conquista;
using Gerenciador.Infra.Data.Context;
using Gerenciador.Service.Common;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    private readonly IEntityDtoMapper<User, CadastroDto> _cadastroMapper;
    private readonly IConquistaRepository _conquistaRepository;
    private readonly GerenciadorContext _gerenciador;

    public UserService(IUserRepository userRepository, IValidator<User> userValidator, IEntityDtoMapper<User, CadastroDto> cadastroMapper, IConquistaRepository conquistaRepository, GerenciadorContext gerenciador)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _cadastroMapper = cadastroMapper;
        _conquistaRepository = conquistaRepository;
        _gerenciador = gerenciador;
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
        var errors = new List<string>();
        var validator = await _userValidator.ValidateAsync(obj);
        obj.Tipo = TipoPessoaEnum.Professor;
        obj.RankId = 8;

        if (user.CodigoCadastro != null && user.OrientadorId != null)
        {
            obj.OrientadorId = user.OrientadorId;
            obj.Tipo = TipoPessoaEnum.Aluno;
            obj.RankId = 1;
            if (await _userRepository.ValidarCodigoCadastro(user.CodigoCadastro))
            {
                await _userRepository.DesativarCodigoCadastro(user.CodigoCadastro);
            }
            else
            {
                errors.Add("Código inválido!");
            }
        }
        
        if (!String.IsNullOrEmpty(user.Password) && !user.Password.Equals(user.Confirm))
            errors.Add("As senhas devem ser iguais");
        
        if(await _userRepository.EmailRegistred(obj.Email))
            errors.Add("Email já cadastrado.");

        if (validator.Errors.Count > 0)
        {
            validator.Errors.ForEach(erro =>
            {
                errors.Add(erro.ErrorMessage);
            });
        }

        if (errors.Count > 0)
        {
            errors = errors.Distinct().ToList();
            string errorMessage = string.Join(Environment.NewLine, errors);
            errorMessage = errorMessage.Replace(Environment.NewLine, ";");
            return new ServiceResult<CadastroDto>() { ErrorMessage = errorMessage, Success = false };
        }
        
        try
        {
            await _userRepository.Insert(obj);
            return new ServiceResult<CadastroDto>() { Data = user };
        }
        catch (Exception ex)
        {
            return new ServiceResult<CadastroDto>() { ErrorMessage = ex.Message, Success = false };
        }
    }

    public async Task<ServiceResult<int>> GerarCodigoCadastro(int userId)
    {
        var codigo = await _userRepository.GerarCodigoCadastro(userId);

        return new ServiceResult<int>()
        {
            Data = codigo
        };
    }

    public async Task<ServiceResult<CodigoCadastroDto>> ValidarCodigoCadastro(int codigo)
    {
        var isValid = await _userRepository.ValidarCodigoCadastro(codigo);

        if (isValid)
        {
            var dto = await _userRepository.GetCodigoCadastro(codigo);
            return new ServiceResult<CodigoCadastroDto>() { Data = dto };
        }

        return new ServiceResult<CodigoCadastroDto>() { Success = false, ErrorMessage = "Código inválido." };
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

    public Task AddPontos(DificuldadeTarefaEnum dificudade, int userId)
    {
        return _userRepository.AddPontos(dificudade, userId);
    }

    public async Task<List<ConquistaDto>> GetConquistas(int userId)
    {
        return await _conquistaRepository.GetConquistasByUserId(userId);
    }

    public async Task AddLink(Link linkDto)
    {
        _gerenciador.Set<Link>().Add(linkDto);
        await _gerenciador.SaveChangesAsync();
    }

    public async Task<List<Link>> GetLinks()
    {
        return await _gerenciador.Set<Link>().ToListAsync();
    }

    public async Task DeleteLink(int linkId)
    {
        var link =  _gerenciador.Set<Link>().Where(l => l.Id == linkId).FirstOrDefault();
        _gerenciador.Set<Link>().Remove(link);
        await _gerenciador.SaveChangesAsync();
    }

    public Task<ServiceResult<T>> GetById<T>(int id)
    {
        throw new NotImplementedException();
    }
    
}