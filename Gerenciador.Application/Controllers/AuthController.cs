using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Application.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private TokenService _tokenService;
    private IUserService _userService;
    private readonly IConfiguration _configuration;
    
    public AuthController(TokenService tokenService, IUserService userService, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userService = userService;
        _configuration = configuration;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<dynamic>> Autenticate([FromBody] User user)
    {
        var usuario = _userService.validate(user);
        if (usuario == null)
        {
            return NotFound(new {message = "Usuário inexistente ou senha inválida"});
        }

        var token = _tokenService.GenerateToken(usuario, _configuration["Jwt:Key"]);
        return Ok(new { token = token });
    }
}