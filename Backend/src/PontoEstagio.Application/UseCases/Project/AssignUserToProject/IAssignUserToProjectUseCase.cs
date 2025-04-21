using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Projects.AssignUserToProject;
public interface IAssignUserToProjectUseCase
{
    Task Execute(Guid projectId, RequestAssignUserToProjectJson request);
}
