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

    public async Task Insert(Tarefa obj)
    {
        obj.DataCriacao = DateTime.Now;
        _dbContext.Set<Tarefa>().Add(obj);
        await _dbContext.SaveChangesAsync();
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
        return await _dbContext.Set<Tarefa>().FindAsync(id);
    }

    public async Task<List<TarefaDto>> GetByUserId(int userId, bool isPrincipal)
    {
        var query = await _dbContext.Set<Tarefa>().Where(t => t.IdPessoa == userId).Include(t => t.CreatedBy).ToListAsync();
        var filtro = isPrincipal ? TipoTarefa.Principal : TipoTarefa.Secundaria;
        query = query.Where(t => t.Tipo == filtro).ToList();
        
        return (List<TarefaDto>)query.Select(t => new TarefaDto()
        {
            Id = t.Id,
            IdPessoa = t.IdPessoa,
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
            DataCriacao = t.DataCriacao,
            DataFinal = t.DataFinal,
        }).ToList();
    }

    public async Task InsertTarefaPrincipal(Tarefa tarefa)
    {
        tarefa.DataCriacao = DateTime.Now;
        _dbContext.Set<Tarefa>().Add(tarefa);
        await _dbContext.SaveChangesAsync();
    }
}