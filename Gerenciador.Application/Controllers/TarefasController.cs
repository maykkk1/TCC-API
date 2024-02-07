using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gerenciador.Domain.Entities;
using Gerenciador.Service.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            await _tarefaService.Add<TarefaValidator>(tarefa);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] Tarefa tarefa)
        {
            await _tarefaService.Update<TarefaValidator>(tarefa);
            return Ok();
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Tarefa>>> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _tarefaService.getByUserId(userId);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("save-principal")]
        public async Task<ActionResult> SavePrincipal([FromBody] Tarefa tarefa)
        {
            // implementar validador no servico
            await _tarefaService.InsertTarefaPrincipal(tarefa);
            return Ok();
        }
    }
}
