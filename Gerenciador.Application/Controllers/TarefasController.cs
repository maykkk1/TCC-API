using System.Security.Claims;
using Gerenciador.Domain.Entities;
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
        [Route("save")]
        public async Task<ActionResult> Save([FromBody] Tarefa tarefa)
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
        public async Task<ActionResult<List<Tarefa>>> Get(bool isPrincipal)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _tarefaService.getByUserId(userId, isPrincipal);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("save-principal")]
        public async Task<ActionResult> SavePrincipal([FromBody] Tarefa tarefa)
        {
            await _tarefaService.InsertTarefaPrincipal(tarefa);
            return Ok();
        }
    }
}
