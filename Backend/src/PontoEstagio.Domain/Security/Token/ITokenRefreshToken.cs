using System.Security.Claims;

namespace PontoEstagio.Domain.Security.Token;

public interface ITokenRefreshToken
{
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
     Guid? ValidateRefreshToken(string token);
}