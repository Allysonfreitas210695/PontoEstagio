using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Security.Token;
using PontoEstagio.Domain.Enum.Extensions;

namespace PontoEstagio.Infrastructure.Security;

public class TokenGenerateAccessToken : ITokenGenerateAccessToken
{
    private readonly IConfiguration _configuration;

    public TokenGenerateAccessToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(User user)
    {
        var key = _configuration["Jwt:Key"]
                  ?? throw new InvalidOperationException("JWT key is not configured.");

        var expiresInStr = _configuration["Jwt:ExpiresInMinutes"]
                           ?? throw new InvalidOperationException("JWT expiration time is not configured.");

        if (!int.TryParse(expiresInStr, out var expiresIn))
            throw new InvalidOperationException("JWT expiration time must be a valid integer.");

        var keyBytes = Encoding.UTF8.GetBytes(key);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Type.ToRoleName()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiresIn),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
