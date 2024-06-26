using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly GerenciadorContext _dbContext;

    public UserRepository(GerenciadorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Insert(User obj)
    {
        _dbContext.Set<User>().Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public async Task Update(User obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _dbContext.Set<User>().Remove(await Select(id));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<User>> Select()
    {
        return await _dbContext.Set<User>().ToListAsync();
    }

    public async Task<User> Select(int id)
    {
        return await _dbContext.Set<User>().FindAsync(id);
    }

    public async Task<User> ValidateLogin(UserLoginDto user)
    {
        var usuario =
            await _dbContext.Set<User>().Include(u => u.Rank).FirstOrDefaultAsync(s => s.Name == user.Name && s.Password == user.Password);
        return usuario;
    }

    public async Task<List<User>> GetOrientandosById(int orientadorId)
    {
        return await _dbContext.Set<User>().Where(t => t.OrientadorId == orientadorId).ToListAsync();
    }

    public async Task<UserDto?> GetAlunoById(int id)
    {
        var query = _dbContext.Set<User>().Where(t => t.Id == id);
        return await query.Select(aluno => new UserDto()
        {
            Id = aluno.Id,
            Name = aluno.Name,
            Tipo = aluno.Tipo
        }).FirstOrDefaultAsync();
    }

    public async Task<bool> EmailRegistred(string email)
    {
        return await _dbContext.Set<User>().AnyAsync(u => u.Email == email);
    }

    public async Task<int> GerarCodigoCadastro(int userId)
    {
        var bd = _dbContext.Set<CodigoCadastro>();
        Random rnd = new Random();
        int novoCodigo;

        do
        {
            novoCodigo = rnd.Next(1000, 10000);
        } while (await bd.AnyAsync(c => c.Codigo == novoCodigo));

        var novoCadastro = new CodigoCadastro
        {
            Codigo = novoCodigo,
            DataExpiracao = DateTime.Now.AddDays(7),
            OrientadorId = userId,
            Status = Status.Ativo
        };

        bd.Add(novoCadastro);
        await _dbContext.SaveChangesAsync();

        return novoCodigo;
    }

    public async Task<bool> ValidarCodigoCadastro(int? codigo)
    {
        var bd = _dbContext.Set<CodigoCadastro>();
        return await bd.Where(c => c.Codigo == codigo &&
                                   c.DataExpiracao > DateTime.Now &&
                                   c.Status == Status.Ativo).AnyAsync();
    }

    public async Task DesativarCodigoCadastro(int? codigo)
    {
        var bd = _dbContext.Set<CodigoCadastro>();
        var entity = await bd.Where(c => c.Codigo == codigo).FirstOrDefaultAsync();
        entity.Status = Status.Inativo;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CodigoCadastroDto> GetCodigoCadastro(int codigo)
    {
        var entity = await _dbContext.Set<CodigoCadastro>().Where(c => c.Codigo == codigo).FirstOrDefaultAsync();
        return new CodigoCadastroDto()
        {
            Codigo = entity.Codigo,
            OrientadorId = entity.OrientadorId
        };
    }

    public async Task<List<IntegranteDto>> GetAllIntegrantes(int projetoId, int orientadorId)
    {
        var alunos = await _dbContext.Set<User>()
            .Include(u => u.Projetos)
            .Include(u => u.ProjetosRelacionados)
            .Where(u => u.OrientadorId == orientadorId).ToListAsync();
        return alunos.Select(u => new IntegranteDto()
        {
            Id = u.Id,
            Name = u.Name,
            Status = u.ProjetosRelacionados.Any(pu => pu.ProjetoId == projetoId) ? Status.Ativo : Status.Inativo
        }).ToList();
    }

    public async Task<List<IntegranteDto>> GetIntegrantes(int projetoId, int orientadorId)
    {
        throw new NotImplementedException();
    }
}