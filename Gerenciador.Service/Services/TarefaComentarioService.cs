using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.TarefasComentario;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class TarefaComentarioService : ITarefaComentarioService
{
    private readonly ITarefaComentarioRepository _tarefaComentarioRepository;
    private readonly IEntityDtoMapper<TarefaComentario, TarefaComentarioDto> _dtoMapper;

    public TarefaComentarioService(ITarefaComentarioRepository tarefaComentarioRepository, IEntityDtoMapper<TarefaComentario, TarefaComentarioDto> dtoMapper)
    {
        _tarefaComentarioRepository = tarefaComentarioRepository;
        _dtoMapper = dtoMapper;
    }
    
    public async Task<ServiceResult<TarefaComentarioDto>> Insert(TarefaComentarioDto comentarioDto)
    {
        var entity = _dtoMapper.DtoToEntity(comentarioDto);
        var response = await _tarefaComentarioRepository.Insert(entity);
        var dto = _dtoMapper.EntityToDto(response);
        return new ServiceResult<TarefaComentarioDto>()
        {
            Data = dto
        };
    }
}