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

    public async Task<List<Project>> GetAllProjectsByInternAsync(User user)
    {
        return await _dbContext.Projects
                            .Where(p => p.UserProjects.Any(up => up.UserId == user.Id))
                            .Include(p => p.Attendances)
                            .Include(p => p.UserProjects)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<List<Project>> GetAllProjectsBySupervisorAsync(User user)
    {
        return await _dbContext.Projects
                            .Where(p => p.CreatedBy == user.Id)
                            .Include(p => p.Attendances)
                            .Include(p => p.UserProjects)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public void Update(Project project)
    {
        _dbContext.Projects.Update(project);
    }

    async Task<Project?> IProjectReadOnlyRepository.GetProjectByIdAsync(Guid id)
    {
        return await _dbContext.Projects
                                .Where(x => x.Id == id)
                                .Include(x => x.Attendances)
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