using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gerenciador.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gerenciador.Service.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
    public string GenerateToken(User user, string keyString)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(keyString);
        var tokenDecriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDecriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token, string keyString)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(keyString);

        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // Pode ajustar de acordo com seus requisitos
            ValidateAudience = false, // Pode ajustar de acordo com seus requisitos
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return true; // Token é válido
        }
        catch (SecurityTokenException)
        {
            return false; // Token é inválido
        }
    }
}