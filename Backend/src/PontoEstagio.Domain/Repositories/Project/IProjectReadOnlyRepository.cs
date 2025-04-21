using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Domain.Repositories.Projects;

public interface IProjectReadOnlyRepository
{
     Task<Entities.Project?> GetProjectByIdAsync(Guid id);
     Task<List<Entities.Project>> GetAllProjectsBySupervisorAsync(Entities.User user);
     Task<List<Entities.Project>> GetAllProjectsByInternAsync(Entities.User user);
}