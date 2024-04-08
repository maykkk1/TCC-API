using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Domain.Entities.Mappers;

public class TarefaMapper : IEntityDtoMapper<Tarefa, TarefaDto>
{
    public Tarefa DtoToEntity(TarefaDto dto)
    {
        return new Tarefa()
        {
            Id = dto.Id,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Situacao = dto.Situacao,
            Tipo = dto.Tipo,
            DataCriacao = dto.DataCriacao,
            DataFinal = dto.DataFinal,
            CreatedById = dto.CreatedById,
            PessoaId = dto.PessoaId
        };
    }

    public TarefaDto EntityToDto(Tarefa entity)
    {
        return new TarefaDto()
        {
            Id = entity.Id,
            Titulo = entity.Titulo,
            Descricao = entity.Descricao,
            Situacao = entity.Situacao,
            Tipo = entity.Tipo,
            DataCriacao = entity.DataCriacao,
            DataFinal = entity.DataFinal,
            CreatedById = entity.CreatedById,
            PessoaId = entity.PessoaId,
            Comentarios = entity.Comentarios.Select(c => new TarefaComentarioDto()
            {
                Id = c.Id,
                Conteudo = c.Conteudo,
                TarefaId = c.TarefaId,
                AutorNome = c.AutorNome,
                AutorId = c.AutorId,
                DataComentario = c.DataComentario
            }).ToList()
        };
    }
}