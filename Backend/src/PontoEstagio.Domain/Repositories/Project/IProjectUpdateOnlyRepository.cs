 namespace PontoEstagio.Domain.Repositories.Projects;
public interface IProjectUpdateOnlyRepository
{
    Task<Entities.Project?> GetProjectByIdAsync(Guid id);
    void Update(Entities.Project project);
}
