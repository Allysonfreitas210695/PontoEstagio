using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class ProjectRepository : IProjectReadOnlyRepository, IProjectWriteOnlyRepository, IProjectUpdateOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;
    public ProjectRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
    }

    public async Task AddUserToProjectAsync(UserProject userProject)
    {
        await _dbContext.UserProjects.AddAsync(userProject);
    }

    public async Task<bool> ExistsProjectAssignedToUserAsync(Guid project_id, Guid userIdToAssign)
    {
        return await _dbContext.UserProjects
                                .AsNoTracking()
                                .AnyAsync(x => x.ProjectId == project_id && x.UserId == userIdToAssign);
    }

    public async Task<List<Project>> GetAllProjectsByInternAsync(User user)
    {
        return await _dbContext.Projects
                            .Where(p => p.UserProjects.Any(up => up.UserId == user.Id))
                            .Include(p => p.Activities)
                            .Include(p => p.UserProjects)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<List<Project>> GetAllProjectsBySupervisorAsync(User user)
    {
        return await _dbContext.Projects
                            .Where(p => p.CreatedBy == user.Id)
                            .Include(p => p.Activities)
                            .Include(p => p.UserProjects)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<Project?> GetCurrentProjectForUserAsync(Guid userId)
    {
        return await _dbContext.UserProjects
                                .Where(up => up.UserId == userId && up.IsCurrent)
                                .Select(up => up.Project)
                                .FirstOrDefaultAsync();
    }

    public void Update(Project project)
    {
        _dbContext.Projects.Update(project);
    }

    async Task<Project?> IProjectReadOnlyRepository.GetProjectByIdAsync(Guid id)
    {
        return await _dbContext.Projects
                                .Where(x => x.Id == id)
                                .Include(x => x.Activities)
                                .Include(x => x.UserProjects)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
    }

    async Task<Project?> IProjectUpdateOnlyRepository.GetProjectByIdAsync(Guid id)
    {
        return await _dbContext.Projects
                                .Where(x => x.Id == id) 
                                .FirstOrDefaultAsync();
    }
}