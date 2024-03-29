using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces.TarefasComentario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefaComentarioController : ControllerBase
    {
        private ITarefaComentarioService _tarefaComentarioService;

        public TarefaComentarioController(ITarefaComentarioService tarefaComentarioService)
        {
            _tarefaComentarioService = tarefaComentarioService;
        }
        
        [HttpPost]
        [Route("save")]
        public async Task<ActionResult> Save([FromBody] TarefaComentarioDto comentario)
        {
            await _tarefaComentarioService.Insert(comentario);
            return Ok();
        }
        
        
    }
}
