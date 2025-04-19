namespace PontoEstagio.Application.UseCases.Projects.DeleteUserFromProject;
public interface IDeleteUserFromProjectUseCase
{
    Task Execute(Guid projectId, Guid userId);
}
