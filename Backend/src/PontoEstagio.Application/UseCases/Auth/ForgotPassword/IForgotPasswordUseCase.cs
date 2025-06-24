using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Auth.ForgotPassword;
public interface IForgotPasswordUseCase
{
    Task Execute(RequestForgotPasswordJson request);
}
