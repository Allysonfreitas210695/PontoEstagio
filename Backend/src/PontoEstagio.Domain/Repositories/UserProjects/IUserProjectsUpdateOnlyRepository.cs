using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Domain.Repositories.UserProjects;

public interface IUserProjectsUpdateOnlyRepository
{
    Task<UserProject?> GetActiveUserProjectAsync(Guid project_id, Guid userIdToAssign);
    void Update(Entities.UserProject userProject);

}
