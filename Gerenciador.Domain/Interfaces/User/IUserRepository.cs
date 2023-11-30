using Gerenciador.Domain.Entities;

namespace Gerenciador.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{ 
    User validate(User user);
}