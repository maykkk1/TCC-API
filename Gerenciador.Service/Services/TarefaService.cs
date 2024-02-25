using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Infra.Data.Context;
using Gerenciador.Service.Common;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Service.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUserRepository _userRepository;
    private readonly GerenciadorContext _dbContext;

    public TarefaService(ITarefaRepository tarefaRepository, IUserRepository userRepository, GerenciadorContext dbContext)
    {
        _tarefaRepository = tarefaRepository;
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<Tarefa> Add(Tarefa obj)
    {
       await _tarefaRepository.Insert(obj);
       return obj;
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<TarefaDto>> Get()
    {
        return await _tarefaRepository.Select();
    }

    public Task<ServiceResult<T>> GetById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Tarefa> Update(Tarefa obj)
    {
        await _tarefaRepository.Update(obj);
        return obj;
    }

    public async Task<List<TarefaDto>> getByUserId(int userId, bool isPrincipal)
    {
        return await _tarefaRepository.GetByUserId(userId, isPrincipal);
    }

    public async Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa)
    {
        await _tarefaRepository.InsertTarefaPrincipal(tarefa);
        return tarefa;
    }

    // ajustar para receber dto e não entidade
    public async Task<ServiceResult<TarefaDto>> UpdateTarefaPrincipal(Tarefa tarefa, int userId)
    {
        var result = new ServiceResult<TarefaDto>();
        var oldTarefa = await _dbContext.Set<Tarefa>().AsNoTracking().Where(t => t.Id == tarefa.Id).FirstOrDefaultAsync();
        var tarefaOrientador = await _dbContext.Set<Tarefa>().AsNoTracking().Where(t => t.IdTarefaRelacionada == oldTarefa.Id).FirstOrDefaultAsync();

        if (tarefaOrientador != null)
        {
            // verificar a logica da tarefa estar indo para analise
            if (tarefaOrientador.Situacao != SituacaoTarefaEnum.Pendente)
            {
                result.Success = false;
                result.ErrorMessage = "Você não pode modificar a situação dessa tarefa pois o orientador já iniciou o processo de avaliação.";
                return result;
            }
        }

        if (oldTarefa == null)
        {
            result.Success = false;
            result.ErrorMessage = "Tarefa não encontrada.";
            return result;
        }

        if (oldTarefa.TarefaRelacionada != null && oldTarefa.TarefaRelacionada.Situacao != SituacaoTarefaEnum.Pendente)
        {
            result.Success = false;
            result.ErrorMessage = "Você não pode modificar o status dessa tarefa pois o orientador já começou o processo de análise dela.";
            return result;
        }
        
        await _tarefaRepository.Update(tarefa);
        if (tarefa.Situacao == SituacaoTarefaEnum.Analise)
        {
            var orientando = _dbContext.Set<User>().Where(u => u.Id == userId).Include(u => u.Orientador).FirstOrDefault();
            var newTarefaOrientador = new Tarefa()
            {
                Titulo = "Tarefa criada depois que aluno pede analise",
                Descricao = "Tarefa criada depois que aluno pede analise",
                Tipo = TipoTarefa.Principal,
                Situacao = SituacaoTarefaEnum.Pendente,
                CreatedById = orientando.Id,
                IdPessoa = orientando.Orientador.Id,
                DataCriacao = DateTime.Now,
                IdTarefaRelacionada = tarefa.Id
            };
            await InsertTarefaPrincipal(newTarefaOrientador);
        }

        result.Success = true;
        //ajustar isso aqui
        result.Data = new TarefaDto();
        return result;
    }
}