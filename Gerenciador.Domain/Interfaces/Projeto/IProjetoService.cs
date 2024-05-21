using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces.Projeto;

public interface IProjetoService
{
    Task<ServiceResult<ProjetoDto>> Add(ProjetoDto dto);
    Task<ServiceResult<List<ProjetoDto>>> GetByUserId(int userId);
    Task<ServiceResult<ProjetoDto>> GetById(int projetoId);
    Task<ServiceResult<TarefaDto>> AddTarefa(TarefaDto tarefa);
    Task<ServiceResult<int>> Delete(int projetoId, int userId);
    Task<ServiceResult<int>> Update(ProjetoDto projeto, int userId);
    Task<string> Validate(Entities.Projeto dto);
    Task<ServiceResult<List<IntegranteDto>>> GetAllIntegrantes(int projetoId, int orientadorId);
    Task<ServiceResult<List<IntegranteDto>>> GetIntegrantes(int projetoId, int orientadorId);
    Task<ServiceResult<bool>> addIntegrant(ProjetoPessoaRelacionamentoDto relacionamento, int orientadorId);
}