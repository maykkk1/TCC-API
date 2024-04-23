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
            .HasColumnName("Id")
            .IsRequired()
            .UseIdentityColumn();
        
        builder.Property(prop => prop.Name)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("varchar(100)");
        
        builder.Property(prop => prop.Sobrenome)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Sobrenome")
            .HasColumnType("varchar(60)");

        builder.Property(prop => prop.Email)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("varchar(100)");
        
        builder.Property(prop => prop.Telefone)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Telefone")
            .HasColumnType("varchar(60)");

        builder.Property(prop => prop.Password)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("varchar(100)");
        
        builder.Property(prop => prop.Tipo)
            .IsRequired()
            .HasColumnName("Tipo")
            .HasColumnType("int");
        
        builder.Property(prop => prop.OrientadorId)
            .HasColumnName("OrientadorId");
        
        builder.HasOne(user => user.Orientador)
            .WithMany()
            .HasForeignKey(user => user.OrientadorId)
            .HasConstraintName("fk_orientador");
    }
}