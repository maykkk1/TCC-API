using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Entities.Mappers;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly ProjetoMapper _projetoMapper;

    public ProjetoService(IProjetoRepository projetoRepository, ProjetoMapper projetoMapper)
    {
        _projetoRepository = projetoRepository;
        _projetoMapper = projetoMapper;
    }

    public async Task<ServiceResult<ProjetoDto>> Add(ProjetoDto dto)
    {
        var obj = _projetoMapper.DtoToEntity(dto);
        await _projetoRepository.Insert(obj);
        return new ServiceResult<ProjetoDto>()
        {
            Data = dto
        };
    }
}