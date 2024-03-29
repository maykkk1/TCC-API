using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class TarefaMap :  IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("tarefa");

        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Descricao)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("varchar(255)");

        builder.Property(prop => prop.Situacao)
            .IsRequired()
            .HasColumnName("Situacao")
            .HasColumnType("int");
        
        builder.Property(prop => prop.Tipo)
            .IsRequired()
            .HasColumnName("tipo")
            .HasColumnType("int");
        
        builder.Property(prop => prop.Titulo)
            .IsRequired()
            .HasColumnName("titulo")
            .HasColumnType("varchar(100)");

        builder.Property(prop => prop.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("date");
        
        builder.Property(prop => prop.DataFinal)
            .HasColumnName("data_final")
            .HasColumnType("date");

        builder.HasOne(tarefa => tarefa.Pessoa)
            .WithMany(user => user.Tarefas)
            .HasForeignKey(tarefa => tarefa.IdPessoa);

        builder.HasOne(tarefa => tarefa.CreatedBy)
            .WithMany(user => user.TarefasCriadas)
            .HasForeignKey(tarefa => tarefa.CreatedById);
        
        builder.HasOne(tarefa => tarefa.TarefaRelacionada)
            .WithMany()
            .HasForeignKey(tarefa => tarefa.IdTarefaRelacionada)
            .IsRequired(false);
    }
}