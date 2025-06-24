using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Cource.Register;

public interface IRegisterCourceUseCase
{
    Task Execute(RequestRegisterCourceJson request);
}