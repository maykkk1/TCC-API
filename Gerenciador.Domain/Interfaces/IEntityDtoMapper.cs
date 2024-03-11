namespace Gerenciador.Domain.Interfaces;

public interface IEntityDtoMapper<TEntity, TDto>
{
    TEntity DtoToEntity(TDto dto);
    TDto EntityToDto(TEntity entity);
}