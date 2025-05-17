using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Login.DoLogin;

public interface ILoginUserUseCase
{
    Task<ResponseLoggedUserJson> Execute(RequestLoginUserJson request);
}