using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Report;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;
public class ReportRepository : IReportReadOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;

    public ReportRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Attendance>> GetByInternAndPeriod(Guid internId, DateTime periodStart, DateTime periodEnd)
    {
        return await _dbContext.Attendances
                                .Where(a =>a.UserId == internId && a.Date >= periodStart && a.Date <= periodEnd)
                                .AsNoTracking()
                                .ToListAsync();
    }

    public async Task<List<Attendance>> GetBySupervisorAndPeriod(Guid supervisorId, DateTime periodStart, DateTime periodEnd)
    {
        return await _dbContext.Attendances
                                .Where(a => 
                                            a.Project.UserProjects.Any(up => up.UserId == supervisorId) && 
                                            a.Date >= periodStart && 
                                            a.Date <= periodEnd)
                                .AsNoTracking()
                                .ToListAsync();
    }
}
