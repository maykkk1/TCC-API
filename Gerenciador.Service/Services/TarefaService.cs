using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Infra.Data.Context;
using Gerenciador.Service.Common;
using Gerenciador.Service.Utils;

namespace Gerenciador.Service.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUserRepository _userRepository;
    private readonly GerenciadorContext _dbContext;
    private readonly IEntityDtoMapper<Tarefa, TarefaDto> _tarefaMapper;

    public TarefaService(ITarefaRepository tarefaRepository, IUserRepository userRepository, GerenciadorContext dbContext, IEntityDtoMapper<Tarefa, TarefaDto> tarefaMapper)
    {
        _tarefaRepository = tarefaRepository;
        _userRepository = userRepository;
        _dbContext = dbContext;
        _tarefaMapper = tarefaMapper;
    }

    public async Task<Tarefa> Add(Tarefa obj)
    {
       await _tarefaRepository.Insert(obj);
       return obj;
    }

    public async Task<ServiceResult<TarefaDto>> Add(TarefaDto obj)
    {
        var entity = _tarefaMapper.DtoToEntity(obj);
        _tarefaRepository.Insert(entity);
        return new ServiceResult<TarefaDto>()
        {
            Data = obj
        };
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<IList<TarefaDto>>> Get()
    {
        var result = new ServiceResult<IList<TarefaDto>>();
        var data = await _tarefaRepository.Select();
        result.Data = data.Select(tarefas => _tarefaMapper.EntityToDto(tarefas)).ToList();
        return result;
    }

    public async Task<ServiceResult<TarefaDto>>GetById(int id)
    {
        var entity = await _tarefaRepository.Select(id);
        return new ServiceResult<TarefaDto>()
        {
            Data = _tarefaMapper.EntityToDto(entity)
        };
    }

    public Task<ServiceResult<TarefaDto>> Update(TarefaDto obj)
    {
        throw new NotImplementedException();
    }

    public async Task<Tarefa> Update(Tarefa obj)
    {
        await _tarefaRepository.Update(obj);
        return obj;
    }

    public async Task<ServiceResult<List<TarefaDto>>> getByUserId(int userId, bool isPrincipal)
    {
        var result = new ServiceResult<List<TarefaDto>>();
        var data = await _tarefaRepository.GetByUserId(userId, isPrincipal);
        result.Data = data;
        return result;
    }

    public async Task<ServiceResult<Tarefa>> InsertTarefaPrincipal(Tarefa tarefa)
    {
        // revisar isso aqui
        await _tarefaRepository.InsertTarefaPrincipal(tarefa);
        var result = new ServiceResult<Tarefa>();
        result.Data = tarefa;
        return result;
    }
    
    public async Task<ServiceResult<TarefaDto>> UpdateTarefaPrincipal(Tarefa tarefa, int userId)
    {
        var user = await _userRepository.Select(userId);
        
        
        await _tarefaRepository.Update(tarefa);
        var dto = _tarefaMapper.EntityToDto(tarefa);
        return new ServiceResult<TarefaDto>()
        {
            Data = dto
        };
    }
}