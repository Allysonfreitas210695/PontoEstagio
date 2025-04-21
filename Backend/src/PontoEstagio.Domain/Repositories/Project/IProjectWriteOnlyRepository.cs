namespace PontoEstagio.Domain.Repositories.Projects;
public interface IProjectWriteOnlyRepository
{
    Task AddAsync(Entities.Project project);
}
