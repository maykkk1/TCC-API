using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces.Projeto;

public interface IProjetoService
{
    Task<ServiceResult<ProjetoDto>> Add(ProjetoDto dto);
    
}