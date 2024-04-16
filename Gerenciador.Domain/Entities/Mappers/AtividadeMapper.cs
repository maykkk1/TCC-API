using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Domain.Entities.Mappers;

public class AtividadeMapper : IEntityDtoMapper<Atividade, AtividadeDto>
{
    public Atividade DtoToEntity(AtividadeDto dto)
    {
        return new Atividade()
        {
            Id = dto.Id,
            Descricao = dto.Descricao,
            PessoaId = dto.PessoaId,
            TarefaId = dto.TarefaId,
            DataAtividade = dto.DataAtividade,
            Tipo = dto.Tipo,
            NovaSituacaoTarefa = dto.NovaSituacaoTarefa
        };
    }

    public AtividadeDto EntityToDto(Atividade entity)
    {
        return new AtividadeDto()
        {
            Id = entity.Id,
            Descricao = entity.Descricao,
            PessoaId = entity.PessoaId,
            TarefaId = entity.TarefaId,
            DataAtividade = entity.DataAtividade,
            Tipo = entity.Tipo,
            TarefaTitulo = entity.Tarefa.Titulo,
            Responsavel = entity.Pessoa.Name,
            NovaSituacaoTarefa = entity.NovaSituacaoTarefa
        };
    }
}