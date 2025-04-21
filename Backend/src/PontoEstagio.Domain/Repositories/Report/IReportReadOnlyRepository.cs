namespace PontoEstagio.Domain.Repositories.Report;

public interface IReportReadOnlyRepository
{
    Task<List<Entities.Attendance>> GetBySupervisorAndPeriod(Guid supervisorId, DateTime periodStart, DateTime periodEnd);
    Task<List<Entities.Attendance>> GetByInternAndPeriod(Guid internId, DateTime periodStart, DateTime periodEnd);
}
