using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class ConquistaPessoaRelacionamentoMap : IEntityTypeConfiguration<ConquistaPessoaRelacionamento>
{
    public void Configure(EntityTypeBuilder<ConquistaPessoaRelacionamento> builder)
    {
        builder.ToTable("CONQUISTA_PESSOA_RELACIONAMENTO");
        
        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.HasOne(relacionamento => relacionamento.Conquista)
            .WithMany(conquista => conquista.PessoasRelacionadas)
            .IsRequired()
            .HasForeignKey(relacionamento => relacionamento.ConquistaId);
        
        
        builder.HasOne(relacionamento => relacionamento.Pessoa)
            .WithMany(pessoa => pessoa.ConquistasRelacionadas)
            .IsRequired()
            .HasForeignKey(relacionamento => relacionamento.PessoaId);
    }
}