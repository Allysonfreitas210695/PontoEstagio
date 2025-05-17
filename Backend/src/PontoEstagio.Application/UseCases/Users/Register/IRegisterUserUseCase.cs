using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseLoggedUserJson> Execute(RequestRegisterUserJson request);
}