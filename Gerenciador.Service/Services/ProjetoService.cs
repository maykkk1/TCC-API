using System.Security.Claims;
using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Atividade;
using Gerenciador.Domain.Interfaces.Conquista;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAtividadeService _atividadeService;
    private readonly IEntityDtoMapper<Tarefa, TarefaDto> _tarefaMapper;
    private readonly IEntityDtoMapper<Projeto, ProjetoDto> _projetoMapper;
    private readonly IValidator<Projeto> _projetoValidator;
    private readonly IConquistaRepository _conquistaRepository;
    

    public ProjetoService(IProjetoRepository projetoRepository, IEntityDtoMapper<Projeto, ProjetoDto> projetoMapper, ITarefaRepository tarefaRepository, IEntityDtoMapper<Tarefa, TarefaDto> tarefaMapper, IValidator<Projeto> projetoValidator, IUserRepository userRepository, IAtividadeService atividadeService, IConquistaRepository conquistaRepository)
    {
        _projetoRepository = projetoRepository;
        _projetoMapper = projetoMapper;
        _tarefaRepository = tarefaRepository;
        _tarefaMapper = tarefaMapper;
        _projetoValidator = projetoValidator;
        _userRepository = userRepository;
        _atividadeService = atividadeService;
        _conquistaRepository = conquistaRepository;
    }

    public async Task<ServiceResult<ProjetoDto>> Add(ProjetoDto dto)
    {
        var obj = _projetoMapper.DtoToEntity(dto);
        var conquistas = await _conquistaRepository.GetConquistasByUserId(dto.OrientadorId);

        if (!conquistas.Any(c => c.Id == 17))
        {
            _conquistaRepository.InsertRelacionamento(dto.OrientadorId, 17);
        }
        var user = await _userRepository.Select(dto.OrientadorId);
        if (user.Tipo != TipoPessoaEnum.Professor)
        {
            return new ServiceResult<ProjetoDto>()
            {
                Success = false,
                ErrorMessage = "Apenas orientadores podem criar novos projetos."
            };
        }
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
        var obj = await _tarefaRepository.Insert(entity);
        await _atividadeService.AddAtividadeProjeto(new AtividadeDto()
        {
            Descricao = "Criação de atividade",
            PessoaId = tarefa.CreatedById,
            TarefaId = obj.Id,
            ProjetoId = tarefa.ProjetoId,
            Tipo = TipoAtividadeEnum.CriacaoTarefa,
            NovaSituacaoTarefa = tarefa.Situacao
        });
        return new ServiceResult<TarefaDto>()
        {
            Data = tarefa
        };
    }

    public async Task<ServiceResult<int>> Delete(int projetoId, int userId)
    {
        var projeto = await _projetoRepository.GetById(projetoId);
        if (projeto.OrientadorId != userId)
        {
            return new ServiceResult<int>()
            {
                Success = false,
                ErrorMessage = "Você não tem autorização para excluir esse projeto."
            };
        }
        await _projetoRepository.Delete(projetoId);
        return new ServiceResult<int>()
        {
            Data = projetoId
        };
    }

    public async Task<ServiceResult<int>> Update(ProjetoDto projeto, int userId)
    {
        if (projeto.OrientadorId != userId)
        {
            return new ServiceResult<int>()
            {
                Success = false,
                ErrorMessage = "Você não tem autorização para atualizar esse projeto."
            };
        }

        var entity = _projetoMapper.DtoToEntity(projeto);
        
        var errorMessage = await Validate(entity);

        if (!String.IsNullOrEmpty(errorMessage))
        {
            return new ServiceResult<int>()
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
        
        await _projetoRepository.Update(entity);
        return new ServiceResult<int>()
        {
            Data = entity.Id
        };
    }

    public async Task<string> Validate(Projeto obj)
    {
        var errors = new List<string>();
        var validator = await _projetoValidator.ValidateAsync(obj);
        string errorMessage = "";
        if (validator.Errors.Count > 0)
        {
            validator.Errors.ForEach(erro =>
            {
                errors.Add(erro.ErrorMessage);
            });
            errors = errors.Distinct().ToList();
            errorMessage = string.Join(Environment.NewLine, errors);
            errorMessage = errorMessage.Replace(Environment.NewLine, ";");
        }
        
        return errorMessage;

    }

    public async Task<ServiceResult<List<IntegranteDto>>> GetAllIntegrantes(int projetoId, int orientadorId)
    {
        var integrantes = await _userRepository.GetAllIntegrantes(projetoId, orientadorId);

        return new ServiceResult<List<IntegranteDto>>()
        {
            Data = integrantes
        };
    }

    public async Task<ServiceResult<List<IntegranteDto>>> GetIntegrantes(int projetoId, int orientadorId)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<bool>> addIntegrant(ProjetoPessoaRelacionamentoDto relacionamento, int orientadorId)
    {
        var projeto = await _projetoRepository.Select(relacionamento.projetoId);
        if (projeto == null)
        {
            return new ServiceResult<bool>()
            {
                Success = false,
                ErrorMessage = "Projeto não encontrado."
            };
        } 
        
        if (projeto.OrientadorId != orientadorId)
        {
            return new ServiceResult<bool>()
            {
                Success = false,
                ErrorMessage = "Apenas o responsável do projeto pode adicionar integrantes."
            };
        }

        if (await _projetoRepository.RelacionamentoExist(relacionamento.projetoId, relacionamento.pessoaId))
        {
            await _projetoRepository.RemoverIntegrante(relacionamento.projetoId, relacionamento.pessoaId);
            return new ServiceResult<bool>()
            {
                Data = false
            };
        }
        
        await _projetoRepository.AddIntegrante(relacionamento.projetoId, relacionamento.pessoaId);
        return new ServiceResult<bool>()
        {
            Data = true
        };
    }
}