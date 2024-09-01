using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class ConquistaMap  : IEntityTypeConfiguration<Conquista>
{
    public void Configure(EntityTypeBuilder<Conquista> builder)
    {
        builder.ToTable("CONQUISTA");

        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Titulo)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Titulo")
            .HasColumnType("varchar(50)");
        
        builder.Property(prop => prop.Descricao)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("varchar(255)");
    }
}