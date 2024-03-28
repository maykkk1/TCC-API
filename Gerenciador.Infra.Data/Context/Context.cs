using Gerenciador.Domain.Entities;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(new UserMap().Configure);
        modelBuilder.Entity<Tarefa>(new TarefaMap().Configure);
        modelBuilder.Entity<TarefaComentario>(new TarefaComentarioMap().Configure);
    }
}