using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Domain.Entities.Mappers;

public class CadastroMapper : IEntityDtoMapper<User, CadastroDto>
{
    public User DtoToEntity(CadastroDto dto)
    {
        return new User()
        {
            Name = dto.Name,
            Sobrenome = dto.Sobrenome,
            Telefone = dto.Telefone,
            Email = dto.Email,
            Tipo = dto.Tipo,
            Password = dto.Password
        };
    }

    public CadastroDto EntityToDto(User entity)
    {
        return null;
    }
}