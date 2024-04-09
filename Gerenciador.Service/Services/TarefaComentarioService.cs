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
    private readonly IUserRepository _userRepository;

    public TarefaComentarioService(ITarefaComentarioRepository tarefaComentarioRepository, IEntityDtoMapper<TarefaComentario, TarefaComentarioDto> dtoMapper, IUserRepository userRepository)
    {
        _tarefaComentarioRepository = tarefaComentarioRepository;
        _dtoMapper = dtoMapper;
        _userRepository = userRepository;
    }
    
    public async Task<ServiceResult<TarefaComentarioDto>> Insert(TarefaComentarioDto comentarioDto)
    {
        var user = await _userRepository.Select(comentarioDto.AutorId);
        comentarioDto.AutorNome = user.Name;
        var entity = _dtoMapper.DtoToEntity(comentarioDto);
        var response = await _tarefaComentarioRepository.Insert(entity);
        var dto = _dtoMapper.EntityToDto(response);
        return new ServiceResult<TarefaComentarioDto>()
        {
            Data = dto
        };
    }

    public async Task<ServiceResult<int>> Delete(int comentarioId, int userId)
    {
        var comentario = await _tarefaComentarioRepository.Select(comentarioId);

        if (comentario == null)
        {
            return new ServiceResult<int>()
            {
                Data = comentarioId,
                ErrorMessage = "Comentario não encontarado",
                Success = false
            };
        }

        if (comentario.AutorId != userId)
        {
            return new ServiceResult<int>()
            {
                Data = comentarioId,
                ErrorMessage = "Só é possível excluir comentários feitos pelo usuário",
                Success = false
            };
        }

        await _tarefaComentarioRepository.Delete(comentarioId);
        
        return new ServiceResult<int>()
        {
            Data = comentarioId
        };
    }
}