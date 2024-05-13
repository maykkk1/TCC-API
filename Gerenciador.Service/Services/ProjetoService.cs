using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IEntityDtoMapper<Tarefa, TarefaDto> _tarefaMapper;
    private readonly IEntityDtoMapper<Projeto, ProjetoDto> _projetoMapper;
    

    public ProjetoService(IProjetoRepository projetoRepository, IEntityDtoMapper<Projeto, ProjetoDto> projetoMapper, ITarefaRepository tarefaRepository, IEntityDtoMapper<Tarefa, TarefaDto> tarefaMapper)
    {
        _projetoRepository = projetoRepository;
        _projetoMapper = projetoMapper;
        _tarefaRepository = tarefaRepository;
        _tarefaMapper = tarefaMapper;
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

    public async Task<ServiceResult<ProjetoDto>> GetById(int projetoId)
    {
        var obj = await _projetoRepository.GetById(projetoId);
        var dto = _projetoMapper.EntityToDto(obj);
        return new ServiceResult<ProjetoDto>()
        {
            Data = dto
        };
    }

    public async Task<ServiceResult<TarefaDto>> AddTarefa(TarefaDto tarefa)
    {
        var entity = _tarefaMapper.DtoToEntity(tarefa);
        await _tarefaRepository.Insert(entity);
        return new ServiceResult<TarefaDto>()
        {
            Data = tarefa
        };
    }
}