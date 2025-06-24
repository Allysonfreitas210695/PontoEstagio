using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.University.Update;

public interface IUpdateUniversityUseCase
{
    Task Execute(Guid projectId, RequestRegisterUniversityJson request);
}