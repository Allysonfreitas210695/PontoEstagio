using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Attendance;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class AttendanceRepository : IAttendanceReadOnlyRepository, IAttendanceUpdateOnlyRepository, IAttendanceWriteOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;
    public AttendanceRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Attendance attendance)
    {
        await _dbContext.AddAsync(attendance);
    }

    public async Task<List<Attendance>> GetAllByInternAsync(Guid intern_id)
    {
        return await _dbContext.Attendances
                                .Where(x => x.UserId == intern_id)
                                .Include(x => x.User)
                                .Include(x => x.Activities)
                                .AsNoTracking()
                                .ToListAsync();
    }

    public Task<List<Attendance>> GetAllBySupervisorAsync(Guid supervisor_id)
    {
        return _dbContext.Attendances
                                .Where(x => x.Activities.Any(y => y.Project.UserProjects.Any(z => z.UserId == supervisor_id)))
                                .Include(x => x.Activities)
                                    .ThenInclude(y => y.Project)
                                    .ThenInclude(y => y.UserProjects)
                                .AsNoTracking()
                                .ToListAsync();
    }

    async Task<Attendance?> IAttendanceUpdateOnlyRepository.GetByIdAsync(Guid id)
    {
        return await _dbContext.Attendances
                                .Where(x => x.Id == id)
                                .Include(x => x.User)
                                .Include(x => x.Activities)
                                    .ThenInclude(y => y.Project)
                                    .ThenInclude(y => y.UserProjects)
                                .FirstOrDefaultAsync();
    }

    async Task<Attendance?> IAttendanceReadOnlyRepository.GetByIdAsync(Guid id)
    {
        return await _dbContext.Attendances
                                .Where(x => x.Id == id)
                                .Include(x => x.User)
                                .Include(x => x.Activities)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
    }

    public async Task<Attendance?> GetByUserIdAndDateAsync(Guid userId, DateTime date)
    {
        return await _dbContext.Attendances
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => 
                                                        a.UserId == userId && 
                                                        a.Date.Date == date.Date
                                                    );
    }

    public void Update(Attendance attendance)
    {
        _dbContext.Attendances.Update(attendance);
    }
}