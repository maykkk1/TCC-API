using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class TarefaMap :  IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("TAREFA");

        builder.Property(prop => prop.Id)
            .HasColumnName("ID")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Descricao)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("DESCRICAO")
            .HasColumnType("varchar(255)");

        builder.Property(prop => prop.Situacao)
            .IsRequired()
            .HasColumnName("SITUACAO")
            .HasColumnType("int");
        
        builder.Property(prop => prop.Dificuldade)
            .IsRequired()
            .HasColumnName("Dificuldade")
            .HasColumnType("int");
        
        builder.Property(prop => prop.Tipo)
            .IsRequired()
            .HasColumnName("TIPO")
            .HasColumnType("int");
        
        builder.Property(prop => prop.Titulo)
            .IsRequired()
            .HasColumnName("TITULO")
            .HasColumnType("varchar(100)");

        builder.Property(prop => prop.DataCriacao)
            .HasColumnName("DATA_CRIACAO")
            .HasColumnType("date");
        
        builder.Property(prop => prop.DataFinal)
            .HasColumnName("DATA_FINAL")
            .HasColumnType("date");

        builder.HasOne(x => x.Pessoa)
            .WithMany(x => x.Tarefas);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.TarefasCriadas);
        
        builder.HasOne(x => x.Projeto)
            .WithMany(x => x.Tarefas);
        
    }
}