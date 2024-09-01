using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Atividade;
using Gerenciador.Domain.Interfaces.Conquista;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Infra.Data.Context;
using Gerenciador.Service.Common;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Service.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IAtividadeService _atividadeService;
    private readonly IProjetoRepository _projetoRepository;
    private readonly IEntityDtoMapper<Tarefa, TarefaDto> _tarefaMapper;
    private readonly GerenciadorContext _gerenciadorContext;
    private readonly IConquistaRepository _conquistaRepository;

    public TarefaService(ITarefaRepository tarefaRepository, IUserRepository userRepository, IEntityDtoMapper<Tarefa, TarefaDto> tarefaMapper, IAtividadeService atividadeService, IProjetoRepository projetoRepository, IUserService userService, GerenciadorContext gerenciadorContext, IConquistaRepository conquistaRepository)
    {
        _tarefaRepository = tarefaRepository;
        _userRepository = userRepository;
        _tarefaMapper = tarefaMapper;
        _atividadeService = atividadeService;
        _projetoRepository = projetoRepository;
        _userService = userService;
        _gerenciadorContext = gerenciadorContext;
        _conquistaRepository = conquistaRepository;
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

    public async Task<ServiceResult<List<TarefaDto>>> getByUserId(int projetoId)
    {
        var result = new ServiceResult<List<TarefaDto>>();
        var data = await _tarefaRepository.GetByUserId(projetoId);
        result.Data = data;
        return result;
    }

    public async Task<ServiceResult<List<TarefaDto>>> getByProjetctId(int projetoId)
    {
        var result = await _tarefaRepository.GetByProjectId(projetoId);
        var dtos = result.Select(tarefas => _tarefaMapper.EntityToDto(tarefas)).ToList();
        return new ServiceResult<List<TarefaDto>>()
        {
            Data = dtos
        };
    }

    public async Task<ServiceResult<Tarefa>> InsertTarefaPrincipal(Tarefa tarefa)
    {
        var obj = await _tarefaRepository.InsertTarefaPrincipal(tarefa);
        
        var atividade = new AtividadeDto()
        {
            Descricao = "Criação de atividade",
            PessoaId = tarefa.CreatedById,
            TarefaId = tarefa.Id,
            Tipo = TipoAtividadeEnum.CriacaoTarefa,
            NovaSituacaoTarefa = tarefa.Situacao
        };
        var pessoasRelacionadas = await _projetoRepository.GetIntegrantesIds(tarefa.ProjetoId); 
        await _atividadeService.Add(atividade, pessoasRelacionadas);

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
        
        var pessoasRelacionadas = await _projetoRepository.GetIntegrantesIds(tarefa.ProjetoId); 
        
        await _atividadeService.Add(atividade, pessoasRelacionadas);
        await _tarefaRepository.Update(tarefa);
        var dto = _tarefaMapper.EntityToDto(tarefa);
        
        if (tarefa.Situacao == SituacaoTarefaEnum.Concluida)
        {
            foreach (var pessoaRelacionada in pessoasRelacionadas)
            {
                tarefa.DataCompleta = DateTime.Now;
                await _userService.AddPontos(tarefa.Dificuldade, pessoaRelacionada);
                await VerificarConquistasTarefasConcluidas(pessoaRelacionada, tarefa.ProjetoId); 
            }
        }
        
        return new ServiceResult<TarefaDto>()
        {
            Data = dto
        };
    }
    
    private async Task VerificarConquistasTarefasConcluidas( int userId, int? projetoId)
    {
        var tarefasConcluidas = await _gerenciadorContext.Set<Tarefa>()
            .Where(t => t.ProjetoId == projetoId && t.Situacao == SituacaoTarefaEnum.Concluida)
            .ToListAsync();
        
        var tarefasConcluidasAntesTempo = await _gerenciadorContext.Set<Tarefa>()
            .Where(t => t.ProjetoId == projetoId && t.Situacao == SituacaoTarefaEnum.Concluida && 
                        t.DataFinal.HasValue &&
                        t.DataFinal > t.DataCompleta)
            .ToListAsync();

        var conquistas = await _conquistaRepository.GetConquistasByUserId(userId);

        // conquistas de tarefas concluidas
        if (tarefasConcluidas.Count > 0 && !conquistas.Any(c => c.Id == 1))
            await _conquistaRepository.InsertRelacionamento(userId, 1);

        if (tarefasConcluidas.Count > 4 && !conquistas.Any(c => c.Id == 14))
            await _conquistaRepository.InsertRelacionamento(userId, 14);

        if (tarefasConcluidas.Count > 9 && !conquistas.Any(c => c.Id == 15))
            await _conquistaRepository.InsertRelacionamento(userId, 15);

        if (tarefasConcluidas.Count > 19 && !conquistas.Any(c => c.Id == 16))
            await _conquistaRepository.InsertRelacionamento(userId, 16); 
        
        if (tarefasConcluidas.Count > 49 && !conquistas.Any(c => c.Id == 4))
            await _conquistaRepository.InsertRelacionamento(userId, 4); 

        
        // conquistas de tarefas concluidas antes do tempo
        if (tarefasConcluidasAntesTempo.Count > 4 && !conquistas.Any(c => c.Id == 2))
            await _conquistaRepository.InsertRelacionamento(userId, 2);

        if (tarefasConcluidasAntesTempo.Count > 9 && !conquistas.Any(c => c.Id == 6))
            await _conquistaRepository.InsertRelacionamento(userId, 6);

        if (tarefasConcluidasAntesTempo.Count > 14 && !conquistas.Any(c => c.Id == 7))
            await _conquistaRepository.InsertRelacionamento(userId, 7);
    }
}