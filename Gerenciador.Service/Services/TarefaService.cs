using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Atividade;
using Gerenciador.Infra.Data.Context;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAtividadeService _atividadeService;
    private readonly IEntityDtoMapper<Tarefa, TarefaDto> _tarefaMapper;

    public TarefaService(ITarefaRepository tarefaRepository, IUserRepository userRepository, IEntityDtoMapper<Tarefa, TarefaDto> tarefaMapper, IAtividadeService atividadeService)
    {
        _tarefaRepository = tarefaRepository;
        _userRepository = userRepository;
        _tarefaMapper = tarefaMapper;
        _atividadeService = atividadeService;
    }

    public async Task<Tarefa> Add(Tarefa obj)
    {
       await _tarefaRepository.Insert(obj);
       return obj;
    }

    public async Task<ServiceResult<TarefaDto>> Add(TarefaDto obj)
    {
        var entity = _tarefaMapper.DtoToEntity(obj);
        await _tarefaRepository.Insert(entity);
        return new ServiceResult<TarefaDto>()
        {
            Data = obj
        };
    }

    public async Task<ServiceResult<int>> Delete(int tarefaId, int userId)
    {
        var tarefa = await _tarefaRepository.Select(tarefaId);

        if (tarefa == null)
        {
            return new ServiceResult<int>()
            {
                Data = tarefaId,
                ErrorMessage = "Tarefa não encontarada",
                Success = false
            };
        }

        if (tarefa.Tipo == TipoTarefa.Principal && tarefa.CreatedById != userId)
        {
            return new ServiceResult<int>()
            {
                Data = tarefaId,
                ErrorMessage = "Apenas o orientador pode excluir essa tarefa",
                Success = false
            };
        } 
        
        if (tarefa.Tipo == TipoTarefa.Secundaria && tarefa.CreatedById != userId)
        {
            return new ServiceResult<int>()
            {
                Data = tarefaId,
                ErrorMessage = "Você não pode excluir essa tarefa",
                Success = false
            };
        }

        await _tarefaRepository.Delete(tarefaId);
        
        return new ServiceResult<int>()
        {
            Data = tarefaId
        };
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
        var obj = await _tarefaRepository.InsertTarefaPrincipal(tarefa);
        
        // tranformar isso em um funcao
        var atividade = new AtividadeDto()
        {
            Descricao = "Criação de atividade",
            PessoaId = tarefa.CreatedById,
            TarefaId = tarefa.Id,
            Tipo = TipoAtividadeEnum.CriacaoTarefa,
            NovaSituacaoTarefa = tarefa.Situacao
        };
        var listaPessoas = new List<int>();
        listaPessoas.Add(tarefa.PessoaId);
        await _atividadeService.Add(atividade, listaPessoas);

        return new ServiceResult<Tarefa>()
        {
            Data = obj
        };
    }
    
    public async Task<ServiceResult<TarefaDto>> UpdateTarefaPrincipal(Tarefa tarefa, int userId)
    {
        var user = await _userRepository.Select(userId);

        if (user.Tipo == TipoPessoaEnum.Aluno && (tarefa.Situacao == SituacaoTarefaEnum.Concluida || tarefa.Situacao == SituacaoTarefaEnum.Retorno))
        {
            return new ServiceResult<TarefaDto>()
            {
                Success = false,
                ErrorMessage = tarefa.Situacao == SituacaoTarefaEnum.Concluida 
                    ? "Apenas o orientador pode concluir tarefas principais."
                    : "Apenas o orientador pode mover a tarefa para o Retorno"
            };
        }

        var atividade = new AtividadeDto()
        {
            Descricao = "Atividade",
            PessoaId = user.Id,
            TarefaId = tarefa.Id,
            Tipo = TipoAtividadeEnum.AlteracaoTarefa,
            NovaSituacaoTarefa = tarefa.Situacao
        };

        var listaPessoas = new List<int>();
        listaPessoas.Add(tarefa.PessoaId);
        await _atividadeService.Add(atividade, listaPessoas);
        await _tarefaRepository.Update(tarefa);
        var dto = _tarefaMapper.EntityToDto(tarefa);
        return new ServiceResult<TarefaDto>()
        {
            Data = dto
        };
    }
}