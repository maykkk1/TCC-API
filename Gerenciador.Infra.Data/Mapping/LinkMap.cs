using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class LinkMap  : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.ToTable("LINK");

        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Descricao)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("varchar(255)");
        
        builder.Property(prop => prop.LinkUrl)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("LinkUrl")
            .HasColumnType("varchar(255)");
    }
}