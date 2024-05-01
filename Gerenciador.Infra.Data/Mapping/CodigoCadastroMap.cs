using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class CodigoCadastroMap : IEntityTypeConfiguration<CodigoCadastro>
{
    public void Configure(EntityTypeBuilder<CodigoCadastro> builder)
    {
        builder.ToTable("CODIGO_CADASTRO");

        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();

        builder.Property(prop => prop.Codigo)
            .HasColumnName("Codigo")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(prop => prop.OrientadorId)
            .HasColumnName("OrientadorId")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(prop => prop.DataExpiracao)
            .HasColumnName("DataExpiracao")
            .HasColumnType("date")
            .IsRequired();

        builder.HasKey(prop => prop.Id).HasName("codigo_cadastro_pkey");
    }
}