using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;

namespace Gerenciador.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{ 
    Task<User> ValidateLogin(UserLoginDto user);
    Task<List<User>> GetOrientandosById(int orientadorId);
    Task<UserDto?> GetAlunoById(int id);
    Task<bool> EmailRegistred(string email);
    Task<int> GerarCodigoCadastro(int userId);

}