using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    Task Execute(Guid id, RequestRegisterUserJson request);
}
