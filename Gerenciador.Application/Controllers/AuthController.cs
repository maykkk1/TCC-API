using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Entities.Dtos;
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
    public async Task<ActionResult<AuthParams>> Autenticate([FromBody] UserLoginDto user)
    {
        var usuario = _userService.ValidateLogin(user);
        // passar tudo isso aqui para um service
        if (usuario == null)
        {
            return Unauthorized(new {message = "Usuário inexistente ou senha inválida"});
        }
        var token = _tokenService.GenerateToken(usuario, _configuration["Jwt:Key"]);

        var auth = new AuthParams()
        {
            Token = token,
            User = new UserDto()
            {
                Name = usuario.Name
            }
        };
            
        return Ok(auth);
    }
}