using System.Security.Claims;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
using Gerenciador.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CadastroDto user)
        {
            var result = await _userService.Cadastrar(user);
            if (result.Success)
            {
                return Ok(result.Data.Id);
            }
            return BadRequest(result.ErrorMessage);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IList<User>> Get()
        {
            var userId = User; 
            return await _userService.Get();
        }

        [HttpGet]
        [Authorize]
        [Route("orientandos")]
        public async Task<ActionResult<List<Tarefa>>> GetOrientandosById()
        {
            var orientadorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _userService.GetOrientandosById(orientadorId);
            return Ok(result);
        }
        
        [HttpGet]
        [Authorize]
        [Route("aluno")]
        public async Task<ActionResult<List<Tarefa>>> GetAlunoById(int id)
        {
            var result = await _userService.GetAlunoById(id);
            return Ok(result);
        }
    }
}
