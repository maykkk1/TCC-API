using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Domain.Entities.Mappers;

public class ProjetoMapper : IEntityDtoMapper<Projeto, ProjetoDto>
{
    public Projeto DtoToEntity(ProjetoDto dto)
    {
        return new Projeto()
        {
            Id = dto.Id,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            OrientadorId = dto.OrientadorId,
            DataCriacao = dto.DataCriacao
        };
    }

    public ProjetoDto EntityToDto(Projeto entity)
    {
        return new ProjetoDto()
        {
            Id = entity.Id,
            Titulo = entity.Titulo,
            Descricao = entity.Descricao,
            OrientadorId = entity.OrientadorId,
            DataCriacao = entity.DataCriacao
        };
    }
}