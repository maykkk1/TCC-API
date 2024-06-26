using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class TarefaRepository : ITarefaRepository
{
    protected readonly GerenciadorContext _dbContext;

    public TarefaRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Tarefa> Insert(Tarefa obj)
    {
        obj.DataCriacao = DateTime.Now;
        _dbContext.Set<Tarefa>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public async Task Update(Tarefa obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<Tarefa>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<Tarefa>> Select()
    {
        return await _dbContext.Set<Tarefa>().ToListAsync();
    }

    public async Task<Tarefa> Select(int id)
    {
        return await _dbContext.Set<Tarefa>().Include(t => t.Comentarios).Where(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<TarefaDto>> GetByUserId(int userId)
    {
        var query = await _dbContext.Set<Tarefa>().Where(t => t.PessoaId == userId)
            .Include(t => t.CreatedBy)
            .Include(t => t.Comentarios)
            .ToListAsync();
        
        return (List<TarefaDto>)query.Select(t => new TarefaDto()
        {
            Id = t.Id,
            PessoaId = t.PessoaId,
            CreatedById = t.CreatedById,
            Titulo = t.Titulo,
            Tipo = t.Tipo,
            Descricao = t.Descricao,
            Situacao = t.Situacao,
            CreatedBy = new UserDto()
            {
                Id = t.CreatedBy.Id,
                Name = t.CreatedBy.Name,
                Tipo = t.CreatedBy.Tipo
            },
            Comentarios = t.Comentarios.Select(c => new TarefaComentarioDto()
            {
                Id = c.Id,
                Conteudo = c.Conteudo,
                TarefaId = c.TarefaId
            }).ToList(),
            DataCriacao = t.DataCriacao,
            DataFinal = t.DataFinal
        }).ToList();
    }

    public async Task<List<Tarefa>> GetByProjectId(int projetoId)
    {
        return  await _dbContext.Set<Tarefa>().Where(t => t.ProjetoId == projetoId)
            .Include(t => t.CreatedBy)
            .Include(t => t.Comentarios)
            .ToListAsync();
    }

    public async Task<Tarefa> InsertTarefaPrincipal(Tarefa tarefa)
    {
        tarefa.DataCriacao = DateTime.Now;
        _dbContext.Set<Tarefa>().Add(tarefa);
        await _dbContext.SaveChangesAsync();
        return tarefa;
    }
}