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

        builder.HasOne(tarefa => tarefa.Pessoa)
            .WithMany()
            .HasForeignKey(tarefa => tarefa.IdPessoa);
    }
}