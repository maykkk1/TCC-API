using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class ProjetoRepository : IProjetoRepository
{
    protected readonly GerenciadorContext _dbContext;

    public ProjetoRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Projeto> Insert(Projeto obj)
    {
        _dbContext.Set<Projeto>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public async Task Update(Projeto obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<Projeto>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<Projeto>> Select()
    {
        return await _dbContext.Set<Projeto>().ToListAsync();
    }

    public async Task<Projeto?> Select(int id)
    {
        return await _dbContext.Set<Projeto>()
            .Include(p => p.Atividades)
            .Where(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddUser(int userId, int projetoId)
    {
        var relacionamento = new ProjetoPessoaRelacionamento()
        {
            PessoaId = userId,
            ProjetoId = projetoId
        };
        _dbContext.Set<ProjetoPessoaRelacionamento>().Add(relacionamento);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Projeto>> GetByUserId(int userId)
    {
        var entities = await _dbContext.Set<ProjetoPessoaRelacionamento>()
            .Include(x => x.Projeto)
            .Where(x => x.PessoaId == userId)
            .Select(x => x.Projeto).ToListAsync();
        return entities;
    }

    public async Task<Projeto> GetById(int projetoId)
    {
        return await _dbContext.Set<Projeto>()
            .Include(p => p.Atividades)
            .ThenInclude(a => a.Pessoa)
            .Include(p => p.Atividades)
            .ThenInclude(a => a.Tarefa)
            .Where(p => p.Id == projetoId)
            .FirstOrDefaultAsync();
    }

    public async Task AddIntegrante(int projetoId, int integranteId)
    {
        _dbContext.Set<ProjetoPessoaRelacionamento>().Add(new ProjetoPessoaRelacionamento()
        {
            PessoaId = integranteId,
            ProjetoId = projetoId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> RelacionamentoExist(int projetoId, int integranteId)
    {
        return await  _dbContext.Set<ProjetoPessoaRelacionamento>()
            .Where(r => r.ProjetoId == projetoId && r.PessoaId == integranteId).AnyAsync();
    }

    public async Task RemoverIntegrante(int projetoId, int integranteId)
    {
        var relacionamento = await _dbContext.Set<ProjetoPessoaRelacionamento>()
            .Where(r => r.ProjetoId == projetoId & r.PessoaId == integranteId).FirstOrDefaultAsync();
        
        _dbContext.Set<ProjetoPessoaRelacionamento>().Remove(relacionamento);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<int>> GetIntegrantesIds(int? projetoId)
    {
        var projeto = _dbContext.Set<Projeto>()
            .Where(x => x.Id == projetoId)
            .Include(x => x.PessoasRelacionadas).FirstOrDefault();
        
        List<int> ids = projeto.PessoasRelacionadas.Select(p => p.PessoaId).ToList();
        
        return ids;
    }
}