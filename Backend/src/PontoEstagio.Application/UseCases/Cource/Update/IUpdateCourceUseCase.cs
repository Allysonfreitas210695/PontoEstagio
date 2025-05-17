using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Cource.Update;

public interface IUpdateCourceUseCase
{
    Task Execute(Guid id, RequestRegisterCourceJson request);
}