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
        
        builder.Property(prop => prop.IdPessoa)
            .HasColumnName("ID_PESSOA");

        builder.HasOne(tarefa => tarefa.Pessoa)
            .WithMany()
            .HasForeignKey(tarefa => tarefa.IdPessoa);
        
        
        builder.Property(prop => prop.CreatedById)
            .HasColumnName("CREATED_BY_ID");
        
        builder.HasOne(tarefa => tarefa.CreatedBy)
            .WithMany()
            .HasForeignKey(tarefa => tarefa.CreatedById);
        
    }
}