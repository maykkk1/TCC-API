using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces.Conquista;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class ConquistaRepository : IConquistaRepository

{
    protected readonly GerenciadorContext _dbContext;

    public ConquistaRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Conquista> Insert(Conquista obj)
    {
        _dbContext.Set<Conquista>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }
    
    public async Task InsertRelacionamento(int userId, int conquistaId)
    {
        _dbContext.Set<ConquistaPessoaRelacionamento>().Add(new ConquistaPessoaRelacionamento()
        {
            PessoaId = userId,
            ConquistaId = conquistaId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task VerificarConquistaRank(int userId, int rankId)
    {
        var conquistas = await GetConquistasByUserId(userId);

        if (rankId == 2 && !conquistas.Any(c => c.Id == 8))
            await InsertRelacionamento(userId, 8);
        
        if (rankId == 3 && !conquistas.Any(c => c.Id == 9))
            await InsertRelacionamento(userId, 9);
        
        if (rankId == 4 && !conquistas.Any(c => c.Id == 10))
            await InsertRelacionamento(userId, 10);
        
        if (rankId == 5 && !conquistas.Any(c => c.Id == 11))
            await InsertRelacionamento(userId, 11);
        
        if (rankId == 6 && !conquistas.Any(c => c.Id == 12))
            await InsertRelacionamento(userId, 12);
        
        if (rankId == 7 && !conquistas.Any(c => c.Id == 13))
            await InsertRelacionamento(userId, 13);


    }

    public async Task Update(Conquista obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<Conquista>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<Conquista>> Select()
    {
        return await _dbContext.Set<Conquista>().ToListAsync();
    }

    public async Task<Conquista> Select(int id)
    {
        return (await _dbContext.Set<Conquista>()
            .Where(t => t.Id == id).FirstOrDefaultAsync())!;
    }

    public async Task<List<ConquistaDto>> GetConquistasByUserId(int userId)
    {
        var totalPessoas = await _dbContext.Set<User>().CountAsync();

        return await _dbContext.Set<ConquistaPessoaRelacionamento>()
            .Where(x => x.PessoaId == userId)
            .Include(x => x.Conquista)
            .Select(x => new ConquistaDto
            {
                Id = x.Conquista.Id,
                Descricao = x.Conquista.Descricao,
                Titulo = x.Conquista.Titulo,
                Porcentagem = (double)_dbContext.Set<ConquistaPessoaRelacionamento>()
                    .Count(cpr => cpr.ConquistaId == x.Conquista.Id) * 100.0 / totalPessoas
            })
            .ToListAsync();
    }
}