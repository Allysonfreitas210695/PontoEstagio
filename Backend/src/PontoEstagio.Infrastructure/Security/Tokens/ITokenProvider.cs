namespace PontoEstagio.Infrastructure.Security.Tokens;

public interface ITokenProvider
{
    string? TokenOnRequest();
}