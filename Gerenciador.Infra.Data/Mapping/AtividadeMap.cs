using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class AtividadeMap : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("ATIVIDADE");

        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Descricao)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("varchar(255)");
        
        builder.Property(prop => prop.DataAtividade)
            .IsRequired()
            .HasColumnName("DataAtividade")
            .HasColumnType("timestamp");
        
        builder.Property(prop => prop.Tipo)
            .IsRequired()
            .HasColumnName("Tipo")
            .HasColumnType("int");
        
        builder.Property(prop => prop.NovaSituacaoTarefa)
            .IsRequired()
            .HasColumnName("NovaSituacaoTarefa")
            .HasColumnType("int");
        
        builder.HasOne(atividade => atividade.Pessoa)
            .WithMany(user => user.AtividadesCriadas)
            .HasForeignKey(atividade => atividade.PessoaId);
        
        builder.HasOne(atividade => atividade.Tarefa)
            .WithMany(tarefa => tarefa.Atividades)
            .HasForeignKey(atividade => atividade.TarefaId);
    }
}