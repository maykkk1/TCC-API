using System.Xml.XPath;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces.Atividade;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class AtividadeRepository : IAtividadeRepository
{
    protected readonly GerenciadorContext _dbContext;

    public AtividadeRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Atividade> Insert(Atividade obj)
    {
        obj.DataAtividade = DateTime.Now;
        _dbContext.Set<Atividade>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public async Task Update(Atividade obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<Atividade>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<Atividade>> Select()
    {
        return await _dbContext.Set<Atividade>().ToListAsync();
    }

    public async Task<Atividade> Select(int id)
    {
        return await _dbContext.Set<Atividade>().FindAsync(id);
    }

    public async Task<List<Atividade>> GetByUserId(int userId)
    {
        var entities = await _dbContext.Set<AtividadePessoaRelacionamento>()
            .Include(x => x.Atividade)
            .ThenInclude(x => x.Tarefa)
            .Include(x => x.Atividade.Pessoa)
            .Where(x => x.PessoaId == userId)
            .Select(x => x.Atividade).ToListAsync();
        return entities;
    }

    public async Task insertRelacionamento(AtividadePessoaRelacionamento relacionamento)
    {
        _dbContext.Set<AtividadePessoaRelacionamento>().Add(relacionamento);
        await _dbContext.SaveChangesAsync();
    }
}