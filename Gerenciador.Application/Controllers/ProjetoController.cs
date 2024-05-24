using System.Security.Claims;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces.Projeto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpPost]
        [Authorize]
        [Route("save")]
        public async Task<ActionResult> Save([FromBody]  ProjetoDto projeto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            projeto.OrientadorId = userId;
            var response = await _projetoService.Add(projeto);
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ProjetoDto>>> Get(int userId)
        {
            var result = await _projetoService.GetByUserId(userId);
            return Ok(result);
        }
        
        [HttpGet]
        [Authorize]
        [Route("projeto")]
        public async Task<ActionResult<ProjetoDto>> GetById(int projetoId)
        {
            var result = await _projetoService.GetById(projetoId);
            return Ok(result);
        }
        
        [HttpGet]
        [Authorize]
        [Route("all-integrantes")]
        public async Task<ActionResult<List<IntegranteDto>>> GetAllIntegrantesByOrientadorId(int projetoId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _projetoService.GetAllIntegrantes(projetoId, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }
        
        [HttpPost]
        [Authorize]
        [Route("tarefa")]
        public async Task<ActionResult> AddTarefa([FromBody] TarefaDto tarefa)
        {
            var result = await _projetoService.AddTarefa(tarefa);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        
        [HttpPost]
        [Authorize]
        [Route("add-integrante")]
        public async Task<ActionResult> AddIntegrante([FromBody] ProjetoPessoaRelacionamentoDto relacionamento)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _projetoService.addIntegrant(relacionamento, userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] int projetoId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _projetoService.Delete(projetoId, userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] ProjetoDto projeto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _projetoService.Update(projeto, userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
