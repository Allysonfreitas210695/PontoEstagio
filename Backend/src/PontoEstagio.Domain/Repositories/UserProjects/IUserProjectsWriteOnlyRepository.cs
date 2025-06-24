namespace PontoEstagio.Domain.Repositories.UserProjects;
public interface IUserProjectsWriteOnlyRepository
{
    Task AddUserToProjectAsync(Entities.UserProject userProject);
}
