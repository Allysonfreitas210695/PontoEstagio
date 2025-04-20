using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Attendance;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class AttendanceRepository : IAttendanceReadOnlyRepository, IAttendanceWriteOnlyRepository
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

    public async Task<Attendance?> GetByIdAsync(Guid id)
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
}