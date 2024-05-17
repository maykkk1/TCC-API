using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces.Projeto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            return Ok(response.Data);
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
    }
}
