using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces;

public interface IUserService : IBaseService<User>
{
    Task<ServiceResult<CadastroDto>> Cadastrar(CadastroDto user);
    Task<User> ValidateLogin(UserLoginDto user);
    Task<List<User>> GetOrientandosById(int orientadorId);
    Task<UserDto> GetAlunoById(int id);
}