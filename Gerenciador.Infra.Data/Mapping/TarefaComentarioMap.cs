using Gerenciador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciador.Infra.Data.Mapping;

public class TarefaComentarioMap : IEntityTypeConfiguration<TarefaComentario>
{
    public void Configure(EntityTypeBuilder<TarefaComentario> builder)
    {
        builder.ToTable("tarefa_comentario");

        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Conteudo)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("conteudo")
            .HasColumnType("TEXT");
        
        builder.HasOne(tarefaComentario => tarefaComentario.Tarefa)
            .WithMany(tarefa => tarefa.Comentarios)
            .HasForeignKey(tarefaComentario => tarefaComentario.IdTarefa);
    }
}