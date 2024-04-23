using System.Security.Claims;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private ITarefaService _tarefaService;

        public TarefasController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost]
        [Authorize]
        [Route("save")]
        public async Task<ActionResult> Save([FromBody] TarefaDto tarefa)
        {
            await _tarefaService.Add(tarefa);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] Tarefa tarefa)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _tarefaService.UpdateTarefaPrincipal(tarefa, userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Tarefa>>> Get(bool isPrincipal, int userId)
        {
            var result = await _tarefaService.getByUserId(userId, isPrincipal);
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize]
        [Route("tarefa")]
        public async Task<ActionResult<List<Tarefa>>> GetTarefaById([FromBody] int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _tarefaService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }
        
        [HttpPost]
        [Authorize]
        [Route("save-principal")]
        public async Task<ActionResult> SavePrincipal([FromBody] Tarefa tarefa)
        {
            await _tarefaService.InsertTarefaPrincipal(tarefa);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _tarefaService.Delete(id, userId);

            if (response.Success)
            {
                return Ok(response.Data);
            };
            
            return BadRequest(response.ErrorMessage);
        }
    }
}
