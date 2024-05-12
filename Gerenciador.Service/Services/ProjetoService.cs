using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Entities.Mappers;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly IEntityDtoMapper<Projeto, ProjetoDto> _projetoMapper;
    

    public ProjetoService(IProjetoRepository projetoRepository, IEntityDtoMapper<Projeto, ProjetoDto> projetoMapper)
    {
        _projetoRepository = projetoRepository;
        _projetoMapper = projetoMapper;
    }

    public async Task<ServiceResult<ProjetoDto>> Add(ProjetoDto dto)
    {
        var obj = _projetoMapper.DtoToEntity(dto);
        obj.DataCriacao = DateTime.Now;
        var projeto = await _projetoRepository.Insert(obj);
        await _projetoRepository.AddUser(projeto.OrientadorId, projeto.Id);
        return new ServiceResult<ProjetoDto>()
        {
            Data = dto
        };
    }

    public async Task<ServiceResult<List<ProjetoDto>>> GetByUserId(int userId)
    {
        var entities = await _projetoRepository.GetByUserId(userId);
        var projetos = entities.Select(projetos => _projetoMapper.EntityToDto(projetos)).ToList();
        return new ServiceResult<List<ProjetoDto>>()
        {
            Data = projetos
        };
    }
}