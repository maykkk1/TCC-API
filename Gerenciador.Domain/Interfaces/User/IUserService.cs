using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;

namespace Gerenciador.Domain.Interfaces;

public interface IUserService : IBaseService<User>
{
    User ValidateLogin(UserLoginDto user);
}