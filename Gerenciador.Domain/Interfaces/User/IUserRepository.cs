using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;

namespace Gerenciador.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{ 
    User ValidateLogin(UserLoginDto user);
}