using Gerenciador.Domain.Entities;

namespace Gerenciador.Domain.Interfaces;

public interface IUserService : IBaseService<User>
{
    User validate(User user);
}