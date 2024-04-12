using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces.Atividade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {

        private IAtividadeService _atividadeService;

        public AtividadeController(IAtividadeService atividadeService)
        {
            _atividadeService = atividadeService;
        }

        [HttpPost]
        [Authorize]
        [Route("save")]
        public async Task<ActionResult> Save([FromBody] AtividadeDto comentario)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _atividadeService.Add(comentario);
            return Ok(response.Data);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AtividadeDto>>> Get(int userId)
        {
            var result = await _atividadeService.getByUserId(userId);
            return Ok(result);
        }
    }
}
