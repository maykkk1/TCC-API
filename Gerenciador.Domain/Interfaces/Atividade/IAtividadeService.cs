using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Service.Common;

namespace Gerenciador.Domain.Interfaces.Atividade;

public interface IAtividadeService
{
    Task<ServiceResult<AtividadeDto>> Add(AtividadeDto dto, List<int?> pessoaIds);

    Task<ServiceResult<int>> Delete(int atividadeId, int userId);

    Task<ServiceResult<IList<AtividadeDto>>> Get();

    public Task<ServiceResult<AtividadeDto>> GetById(int id);

    Task<ServiceResult<AtividadeDto>> Update(AtividadeDto obj);
    Task<ServiceResult<List<AtividadeDto>>> getByUserId(int userId);
}