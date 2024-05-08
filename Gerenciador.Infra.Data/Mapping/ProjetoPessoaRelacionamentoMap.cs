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
        
        builder.HasOne(relacionamento => relacionamento.Projeto)
            .WithMany(projeto => projeto.PessoasRelacionadas)
            .IsRequired()
            .HasForeignKey(relacionamento => relacionamento.ProjetoId);
        
        
        builder.HasOne(relacionamento => relacionamento.Pessoa)
            .WithMany(pessoa => pessoa.ProjetosRelacionados)
            .IsRequired()
            .HasForeignKey(relacionamento => relacionamento.PessoaId);
    }
}