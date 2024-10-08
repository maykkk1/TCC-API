using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Entities.Mappers;
using Gerenciador.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Infra.Data.Context;

public class GerenciadorContext : DbContext
{
    public GerenciadorContext(DbContextOptions<GerenciadorContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<TarefaComentario> TarefaComentarios { get; set; }
    public DbSet<Atividade> Atividade { get; set; }
    public DbSet<AtividadePessoaRelacionamento> AtividadePessoaRelacionamentos { get; set; }
    public DbSet<CodigoCadastro> CodigosCadastro { get; set; }
    public DbSet<Projeto> Projeto { get; set; }
    public DbSet<ProjetoPessoaRelacionamento> ProjetoPessoaRelacionamentos { get; set; }
    public DbSet<Ranks> Ranks { get; set; }
    public DbSet<Conquista> Conquistas { get; set; }
    public DbSet<ConquistaPessoaRelacionamento> ConquistaPessoaRelacionamentos { get; set; }
    public DbSet<Link> Links { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(new UserMap().Configure);
        modelBuilder.Entity<Tarefa>(new TarefaMap().Configure);
        modelBuilder.Entity<TarefaComentario>(new TarefaComentarioMap().Configure);
        modelBuilder.Entity<Atividade>(new AtividadeMap().Configure);
        modelBuilder.Entity<AtividadePessoaRelacionamento>(new AtividadePessoaRelacionamentoMap().Configure);
        modelBuilder.Entity<CodigoCadastro>(new CodigoCadastroMap().Configure);
        modelBuilder.Entity<Projeto>(new ProjetoMap().Configure);
        modelBuilder.Entity<ProjetoPessoaRelacionamento>(new ProjetoPessoaRelacionamentoMap().Configure);
        modelBuilder.Entity<Ranks>(new RanksMap().Configure);
        modelBuilder.Entity<Conquista>(new ConquistaMap().Configure);
        modelBuilder.Entity<ConquistaPessoaRelacionamento>(new ConquistaPessoaRelacionamentoMap().Configure);
        modelBuilder.Entity<Link>(new LinkMap().Configure);
    }
}