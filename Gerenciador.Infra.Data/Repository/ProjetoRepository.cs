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

    public Task Update(Projeto obj)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Projeto>> Select()
    {
        throw new NotImplementedException();
    }

    public Task<Projeto> Select(int id)
    {
        throw new NotImplementedException();
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
        return await _dbContext.Set<Projeto>().Where(p => p.Id == projetoId).FirstOrDefaultAsync();
    }
}