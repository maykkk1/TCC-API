using System.Security.Claims;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Enums;
using Gerenciador.Domain.Interfaces.Atividade;
using Gerenciador.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {

        private IAtividadeService _atividadeService;
        private readonly GerenciadorContext _context;

        public AtividadeController(IAtividadeService atividadeService, GerenciadorContext context)
        {
            _atividadeService = atividadeService;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        [Route("notificacao")]
        public async Task<ActionResult> Save([FromBody] NotificacaoDto notificacao)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var atividade = new AtividadeDto()
            {
                Descricao = notificacao.Descricao,
                Tipo = TipoAtividadeEnum.Notificacao,
                Responsavel = "Administração",
                PessoaId = userId
            };

            var ids = await _context.Set<User>().Select(u => u.Id).ToListAsync();
            
            var response = await _atividadeService.Add(atividade, ids);
            return Ok(response.Data);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AtividadeDto>>> Get(int userId)
        {
            var result = await _atividadeService.GetByUserId(userId);
            return Ok(result);
        }
    }
}
