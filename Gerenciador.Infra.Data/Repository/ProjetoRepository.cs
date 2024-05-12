using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces.Projeto;
using Gerenciador.Infra.Data.Context;

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
}