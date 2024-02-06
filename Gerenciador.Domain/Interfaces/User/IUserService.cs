using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;

namespace Gerenciador.Domain.Interfaces;

public interface IUserService : IBaseService<User>
{
    Task<User> ValidateLogin(UserLoginDto user);
    Task<List<User>> GetOrientandosById(int orientadorId);
}