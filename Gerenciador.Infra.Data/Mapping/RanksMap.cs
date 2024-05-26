using Gerenciador.Domain.Entities.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class RanksMap : IEntityTypeConfiguration<Ranks>
{
    public void Configure(EntityTypeBuilder<Ranks> builder)
    {
        builder.ToTable("RANKS");

        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Nome)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("varchar(100)");
        
        builder.Property(prop => prop.Tipo)
            .IsRequired()
            .HasColumnName("Tipo")
            .HasColumnType("int");
        
        builder.Property(prop => prop.PontosMinimos)
            .IsRequired()
            .HasColumnName("PontosMinimos")
            .HasColumnType("int");
        
        builder.Property(prop => prop.PontosMaximos)
            .IsRequired()
            .HasColumnName("PontosMaximos")
            .HasColumnType("int");
    }
}