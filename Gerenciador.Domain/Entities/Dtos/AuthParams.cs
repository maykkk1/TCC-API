namespace Gerenciador.Domain.Entities.Dtos;

public class AuthParams
{
    public string Token { get; set; }
    public UserDto User  { get; set; }
}