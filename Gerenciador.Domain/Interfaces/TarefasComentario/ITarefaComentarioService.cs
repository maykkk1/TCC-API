using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces.TarefasComentario;

public interface ITarefaComentarioService 
{
    Task<ServiceResult<TarefaComentarioDto>> Insert(TarefaComentarioDto comentarioDto);
    Task<ServiceResult<int>> Delete(int comentarioId, int userId);
}