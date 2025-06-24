using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Domain.Security.Token;
public interface ITokenGenerateAccessToken
{
    string GenerateAccessToken(User user);
}
