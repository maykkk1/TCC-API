using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class TarefaComentarioMap : IEntityTypeConfiguration<TarefaComentario>
{
    public void Configure(EntityTypeBuilder<TarefaComentario> builder)
    {
        builder.ToTable("TAREFA_COMENTARIO");

        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Conteudo)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("CONTEUDO")
            .HasColumnType("TEXT");
        
        builder.HasOne(comentario => comentario.Tarefa)
            .WithMany(tarefa => tarefa.Comentarios)
            .HasForeignKey(comentario => comentario.IdTarefa);
    }
}