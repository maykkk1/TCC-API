using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;

namespace Gerenciador.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{ 
    Task<User> ValidateLogin(UserLoginDto user);
    Task<List<User>> GetOrientandosById(int orientadorId);
    Task<UserDto?> GetAlunoById(int id);
    Task<bool> EmailRegistred(string email);
    Task<int> GerarCodigoCadastro(int userId);
    Task<bool> ValidarCodigoCadastro(int? codigo);
    Task DesativarCodigoCadastro(int? codigo);
    Task<CodigoCadastroDto> GetCodigoCadastro(int codigo);
    Task<List<IntegranteDto>> GetAllIntegrantes(int projetoId, int orientadorId);
    Task<List<IntegranteDto>> GetIntegrantes(int projetoId, int orientadorId);
    Task AddPontos(DificuldadeTarefaEnum dificudade, int userId);

}