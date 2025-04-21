using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Projects.Register;
public interface IRegisterProjectUseCase
{
    Task<ResponseShortProjectJson> Execute(RequestRegisterProjectJson request);
}
