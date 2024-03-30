using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USER");

        builder.Property(prop => prop.Id)
            .HasColumnName("ID")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Name)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("NAME")
            .HasColumnType("varchar(100)");

        builder.Property(prop => prop.Email)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("EMAIL")
            .HasColumnType("varchar(100)");

        builder.Property(prop => prop.Password)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("PASSWORD")
            .HasColumnType("varchar(50)");
        
        builder.Property(prop => prop.Tipo)
            .IsRequired()
            .HasColumnName("TIPO")
            .HasColumnType("int");
        
        builder.Property(prop => prop.OrientadorId)
            .HasColumnName("ORIENTADOR_ID");
        
        builder.HasOne(user => user.Orientador)
            .WithMany()
            .HasForeignKey(user => user.OrientadorId)
            .HasConstraintName("fk_orientador");
    }
}