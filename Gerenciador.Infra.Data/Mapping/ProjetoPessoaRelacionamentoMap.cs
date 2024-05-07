using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class ProjetoPessoaRelacionamentoMap  : IEntityTypeConfiguration<ProjetoPessoaRelacionamento>
{
    public void Configure(EntityTypeBuilder<ProjetoPessoaRelacionamento> builder)
    {
        builder.ToTable("PROJETO_PESSOA_RELACIONAMENTO");
        
        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.HasOne(relacionamento => relacionamento.Atividade)
            .WithMany(atividade => atividade.PessoasRelacionadas)
            .IsRequired()
            .HasForeignKey(relacionamento => relacionamento.AtividadeId);
        
        
        builder.HasOne(relacionamento => relacionamento.Pessoa)
            .WithMany(pessoa => pessoa.AtividadesRelacionadas)
            .IsRequired()
            .HasForeignKey(relacionamento => relacionamento.PessoaId);
    }
}