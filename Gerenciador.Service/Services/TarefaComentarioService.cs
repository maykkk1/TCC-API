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

    public TarefaComentarioService(ITarefaComentarioRepository tarefaComentarioRepository)
    {
        _tarefaComentarioRepository = tarefaComentarioRepository;
    }
    
    public async Task<ServiceResult<int>> Insert(TarefaComentarioDto comentarioDto)
    {
        var entity = _dtoMapper.DtoToEntity(comentarioDto);
        await _tarefaComentarioRepository.Insert(entity);
        return new ServiceResult<int>()
        {
            Data = comentarioDto.Id
        };
    }
}