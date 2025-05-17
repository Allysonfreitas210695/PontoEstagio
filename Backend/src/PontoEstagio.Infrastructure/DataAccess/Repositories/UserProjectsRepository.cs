using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.UserProjects;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;
public class UserProjectsRepository : IUserProjectsReadOnlyRepository, IUserProjectsUpdateOnlyRepository, IUserProjectsWriteOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;
    public UserProjectsRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserToProjectAsync(UserProject userProject)
    {
        await _dbContext.UserProjects.AddAsync(userProject);
    }

    public async Task<bool> ExistsProjectAssignedToUserAsync(Guid project_id, Guid userIdToAssign)
    {
        return await _dbContext.UserProjects
                                .AsNoTracking()
                                .AnyAsync(x => 
                                                x.ProjectId == project_id && 
                                                x.UserId == userIdToAssign &&
                                                x.IsCurrent
                                         );
    }

    public async Task<UserProject?> GetActiveUserProjectAsync(Guid projectId, Guid userId)
    {
        return await _dbContext.UserProjects
                            .Where(up =>
                                    up.ProjectId == projectId &&
                                    up.UserId == userId &&
                                    up.IsCurrent
                            ) 
                            .FirstOrDefaultAsync();
    }

    public async Task<Project?> GetCurrentProjectForUserAsync(Guid userId)
    {
        return await _dbContext.UserProjects
                                .Where(up => up.UserId == userId && up.IsCurrent)
                                .Select(up => up.Project)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
    }

    public void Update(UserProject userProject)
    {
        _dbContext.UserProjects.Update(userProject);
    }
}
