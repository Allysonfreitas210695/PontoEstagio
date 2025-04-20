namespace PontoEstagio.Domain.Repositories.Activity;

public interface IActivityReadOnlyRepository
{
    Task<List<Entities.Activity>> GetByInternIdAsync(Guid internId);
    Task<List<Entities.Activity>> GetBySupervisorIdAsync(Guid supervisorId);
    Task<List<Entities.Activity>> GetByAttendanceIdAsync(Guid attendanceId);
    Task<List<Entities.Activity>> GetByProjectIdAsync(Guid projectId);
    Task<Entities.Activity?> GetByIdAsync(Guid id);
}