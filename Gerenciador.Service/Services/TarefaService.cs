using FluentValidation;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Infra.Data.Context;
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

    public async Task<Tarefa> Add<TValidator>(Tarefa obj) where TValidator : AbstractValidator<Tarefa>
    {
       await _tarefaRepository.Insert(obj);
       return obj;
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Tarefa>> Get()
    {
        return await _tarefaRepository.Select();
    }

    public Task<Tarefa> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Tarefa> Update<TValidator>(Tarefa obj) where TValidator : AbstractValidator<Tarefa>
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

    public async Task<Tarefa> UpdateTarefaPrincipal(Tarefa tarefa, int userId)
    {
        await _tarefaRepository.Update(tarefa);
        if (tarefa.Situacao == SituacaoTarefaEnum.Analise)
        {
            var orientando = _dbContext.Set<User>().Where(u => u.Id == userId).Include(u => u.Orientador).FirstOrDefault();
            var tarefaOrientador = new Tarefa()
            {
                Titulo = "Tarefa criada depois que aluno pede analise",
                Descricao = "Tarefa criada depois que aluno pede analise",
                Tipo = TipoTarefa.Principal,
                Situacao = SituacaoTarefaEnum.Pendente,
                CreatedById = orientando.Id,
                IdPessoa = orientando.Orientador.Id,
                DataCriacao = DateTime.Now
            };
            await InsertTarefaPrincipal(tarefaOrientador);
        }
        return tarefa;
    }
}