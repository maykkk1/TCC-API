using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class ProjetoMap : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.ToTable("PROJETO");

        builder.Property(prop => prop.Id)
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Descricao)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("varchar(255)");
        
        builder.Property(prop => prop.DataCriacao)
            .IsRequired()
            .HasColumnName("DataCriacao")
            .HasColumnType("timestamp");
        
        builder.Property(prop => prop.OrientadorId)
            .IsRequired()
            .HasColumnName("OrientadorId")
            .HasColumnType("int");
    }
}