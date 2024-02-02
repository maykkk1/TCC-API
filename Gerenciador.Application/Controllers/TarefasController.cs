using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gerenciador.Domain.Entities;
using Gerenciador.Service.Validators;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> save([FromBody] Tarefa tarefa)
        {
            _tarefaService.Add<TarefaValidator>(tarefa);
            return Ok();
        }
    }
}
