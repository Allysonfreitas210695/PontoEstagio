namespace PontoEstagio.Application.UseCases.Projects.DeleteUserFromProject;
public interface IDeleteUserFromProjectUseCase
{
    Task Execute(Guid projectId, Guid Intern_Id, Guid Supervisor_Id);
}
