using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PontoEstagio.Domain.Security.Token;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.Security.Tokens;

public class TokenRefreshToken : ITokenRefreshToken
{
    private readonly PontoEstagioDbContext _dbContext;
     private readonly IConfiguration _configuration;
     
    public TokenRefreshToken(
        PontoEstagioDbContext dbContext, 
        IConfiguration configuration
    )
    { 
        _dbContext = dbContext;
        _configuration = configuration;
    }
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }

    public Guid? ValidateRefreshToken(string token)
    {
         var refreshToken = _dbContext.UserRefreshTokens
                                    .FirstOrDefault(urt => urt.Token == token);

         if (refreshToken == null || refreshToken.ExpirationDate < DateTime.UtcNow)
            return null;

         return refreshToken.UserId;
    }
 
}