using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Projects.Update;
public interface IUpdateProjectUseCase
{
    Task Execute(Guid projectId, RequestRegisterProjectJson request);
}
