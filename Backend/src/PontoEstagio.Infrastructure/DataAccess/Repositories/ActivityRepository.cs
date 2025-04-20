using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class ActivityRepository : IActivityReadOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;
    public ActivityRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Activity>> GetByAttendanceIdAsync(Guid attendanceId)
    {
        return await _dbContext.Activitys
                            .Where(x => x.AttendanceId == attendanceId)
                            .Include(x => x.Project)
                                .ThenInclude(y => y.UserProjects)
                            .Include(x => x.Attendance)
                            .AsNoTracking()
                            .ToListAsync();
    }
    
    public Task<Activity?> GetByIdAsync(Guid id)
    {
        return _dbContext.Activitys
                            .Where(x => x.Id == id)
                            .Include(x => x.Project)
                                .ThenInclude(y => y.UserProjects)
                            .Include(x => x.Attendance)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
    }

    public async Task<List<Activity>> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbContext.Activitys
                            .Where(x => x.ProjectId == projectId)
                            .Include(x => x.Project)
                                .ThenInclude(y => y.UserProjects)
                            .Include(x => x.Attendance)
                            .AsNoTracking()
                            .ToListAsync();
    }
}