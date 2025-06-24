using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Auth.Refresh;

public interface IRefreshTokenUseCase
{
    Task<ResponseLoggedUserJson> Execute(RequestRefreshTokenJson request);
}