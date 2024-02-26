using AutoMapper;

namespace Gerenciador.Service.Utils;

public class DtoEntityMapper<TDto, TEntity>
{
    private readonly IMapper _mapper;

    public DtoEntityMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TDto, TEntity>();
            cfg.CreateMap<TEntity, TDto>();
        });
        
        _mapper = config.CreateMapper();
    }

    public TEntity ToEntity(TDto dto)
    {
        return _mapper.Map<TDto, TEntity>(dto);
    }

    public TDto ToDto(TEntity entity)
    {
        return _mapper.Map<TEntity, TDto>(entity);
    }
}