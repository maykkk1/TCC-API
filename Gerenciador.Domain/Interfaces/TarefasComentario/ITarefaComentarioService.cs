using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces.TarefasComentario;

public interface ITarefaComentarioService 
{
    Task<ServiceResult<int>> Insert(TarefaComentarioDto comentarioDto);
}