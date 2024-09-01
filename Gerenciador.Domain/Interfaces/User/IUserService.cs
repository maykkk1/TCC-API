using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces;

public interface IUserService : IBaseService<User>
{
    Task<ServiceResult<CadastroDto>> Cadastrar(CadastroDto user);
    Task<ServiceResult<int>> GerarCodigoCadastro(int userId);
    Task<ServiceResult<CodigoCadastroDto>> ValidarCodigoCadastro(int codigo);
    Task<User> ValidateLogin(UserLoginDto user);
    Task<List<User>> GetOrientandosById(int orientadorId);
    Task<UserDto> GetAlunoById(int id);
    Task AddPontos(DificuldadeTarefaEnum dificudade, int userId);
    Task<List<ConquistaDto>> GetConquistas(int userId);
    Task AddLink(Link link);
    Task<List<Link>> GetLinks();
}