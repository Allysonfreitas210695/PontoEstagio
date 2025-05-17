using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class ActivityRepository : IActivityReadOnlyRepository, IActivityWriteOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;
    public ActivityRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Activity activity)
    {
        await _dbContext.AddAsync(activity);
    }

    public async Task<List<Activity>> GetByAttendanceIdAsync(Guid attendanceId)
    {
        return await _dbContext.Activitys
                            .Where(x => x.AttendanceId == attendanceId)
                            .Include(x => x.Attendance)
                            .AsNoTracking()
                            .ToListAsync();
    }
    
    public Task<Activity?> GetByIdAsync(Guid id)
    {
        return _dbContext.Activitys
                            .Where(x => x.Id == id)
                            .Include(x => x.Attendance)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
    }

    public async Task<List<Activity>> GetByInternIdAsync(Guid internId)
    {
        return await _dbContext.Activitys
                            .Where(x => x.Attendance.Project.UserProjects.Any(y => y.UserId == internId))
                            .Include(x => x.Attendance)
                                .ThenInclude(y => y.Project)
                                .ThenInclude(y => y.UserProjects)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<List<Activity>> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbContext.Activitys
                            .Where(x => x.Attendance.Project.Id == projectId)
                            .Include(x => x.Attendance)
                                .ThenInclude(y => y.Project)
                                .ThenInclude(y => y.UserProjects)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<List<Activity>> GetBySupervisorIdAsync(Guid supervisorId)
    {
        return await _dbContext.Activitys
                            .Where(x => x.Attendance.Project.UserProjects.Any(y => y.UserId == supervisorId))
                            .Include(x => x.Attendance)
                                .ThenInclude(y => y.Project)
                                .ThenInclude(y => y.UserProjects)
                            .AsNoTracking()
                            .ToListAsync();
    }
}