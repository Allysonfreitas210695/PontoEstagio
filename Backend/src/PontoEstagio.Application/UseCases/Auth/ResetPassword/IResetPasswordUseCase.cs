using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Auth.ResetPassword;
public interface IResetPasswordUseCase
{
    Task Execute(RequestResetPasswordJson request);
}
