using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Domain.Interfaces.Atividade;
using Gerenciador.Service.Common;

namespace Gerenciador.Service.Services;

public class AtividadeService : IAtividadeService
{
    private readonly IAtividadeRepository _atividadeRepository;
    private readonly IEntityDtoMapper<Atividade, AtividadeDto> _atividadeMapper;

    public AtividadeService(IAtividadeRepository atividadeRepository, IEntityDtoMapper<Atividade, AtividadeDto> atividadeMapper)
    {
        _atividadeRepository = atividadeRepository;
        _atividadeMapper = atividadeMapper;
    }

    public async Task<ServiceResult<AtividadeDto>> Add(AtividadeDto dto)
    {
        var obj = _atividadeMapper.DtoToEntity(dto);
        await _atividadeRepository.Insert(obj);
        return new ServiceResult<AtividadeDto>()
        {
            Data = dto
        };
    }

    public Task<ServiceResult<int>> Delete(int atividadeId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<IList<AtividadeDto>>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<AtividadeDto>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<AtividadeDto>> Update(AtividadeDto obj)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<AtividadeDto>>> getByUserId(int userId)
    {
        var entities = await _atividadeRepository.GetByUserId(userId);
        
        var atividades = entities.Select(atividades => _atividadeMapper.EntityToDto(atividades)).ToList();

        return new ServiceResult<List<AtividadeDto>>()
        {
            Data = atividades
        };
    }
}