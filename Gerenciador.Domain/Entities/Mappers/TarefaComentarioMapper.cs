using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Domain.Entities.Mappers;

public class TarefaComentarioMapper : IEntityDtoMapper<TarefaComentario, TarefaComentarioDto>
{
    public TarefaComentario DtoToEntity(TarefaComentarioDto dto)
    {
        return new TarefaComentario()
        {
            Id = dto.Id,
            Conteudo = dto.Conteudo,
            IdTarefa = dto.IdTarefa
        };
    }

    public TarefaComentarioDto EntityToDto(TarefaComentario entity)
    {
        return new TarefaComentarioDto()
        {
            Id = entity.Id,
            Conteudo = entity.Conteudo,
            IdTarefa = entity.IdTarefa
        };
    }
}