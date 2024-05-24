using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;

namespace Gerenciador.Domain.Entities.Mappers;

public class ProjetoMapper : IEntityDtoMapper<Projeto, ProjetoDto>
{
    public Projeto DtoToEntity(ProjetoDto dto)
    {
        return new Projeto()
        {
            Id = dto.Id,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            OrientadorId = dto.OrientadorId,
            DataCriacao = dto.DataCriacao
        };
    }

    public ProjetoDto EntityToDto(Projeto entity)
    {
        return new ProjetoDto()
        {
            Id = entity.Id,
            Titulo = entity.Titulo,
            Descricao = entity.Descricao,
            OrientadorId = entity.OrientadorId,
            DataCriacao = entity.DataCriacao,
            
            Atividades = entity.Atividades == null 
                ? new List<AtividadeDto>()
                :
                entity.Atividades.Select(a => new AtividadeDto()
            {
                Id = a.Id,
                Descricao = a.Descricao,
                DataAtividade = a.DataAtividade,
                Tipo = a.Tipo, 
                Responsavel = a.Pessoa?.Name,
                TarefaTitulo = a.Tarefa?.Titulo,
                PessoaId = a.PessoaId,
                TarefaId = a.TarefaId,
                ProjetoId = a.ProjetoId,
                NovaSituacaoTarefa = a.NovaSituacaoTarefa
            }).ToList()
        };
    }
}