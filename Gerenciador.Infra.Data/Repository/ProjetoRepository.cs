using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces.Projeto;

namespace Gerenciador.Infra.Data.Repository;

public class ProjetoRepository : IProjetoRepository
{
    public Task<Projeto> Insert(Projeto obj)
    {
        throw new NotImplementedException();
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