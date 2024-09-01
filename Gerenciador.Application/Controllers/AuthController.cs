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
    
    public AuthController(TokenService tokenService, IUserService userService, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userService = userService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthParams>> Autenticate([FromBody] UserLoginDto user)
    {
        var usuario = await _userService.ValidateLogin(user);
        // passar tudo isso aqui para um service
        if (usuario == null)
        {
            return Unauthorized(new {message = "Usuário inexistente ou senha inválida"});
        }
        var token = _tokenService.GenerateToken(usuario);

        var auth = new AuthParams()
        {
            Token = token,
            User = new UserDto()
            {
                Id = usuario.Id,
                Name = usuario.Name,
                Tipo = usuario.Tipo,
                Pontos = usuario.Pontos,
                Sobrenome = usuario.Sobrenome,
                email = usuario.Email,
                Telefone = usuario.Telefone,
                Rank = new RankDto()
                {
                    Id = usuario.Rank.Id,
                    Nome = usuario.Rank.Nome,
                    Tipo = usuario.Rank.Tipo,
                    PontosMaximos = usuario.Rank.PontosMaximos,
                    PontosMinimos = usuario.Rank.PontosMinimos
                }
            }
        };
            
        return Ok(auth);
    }
}