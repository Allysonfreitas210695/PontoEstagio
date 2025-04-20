namespace PontoEstagio.Domain.Repositories.Activity;

public interface IActivityReadOnlyRepository
{
    Task<List<Entities.Activity>> GetByAttendanceIdAsync(Guid attendanceId);
    Task<Entities.Activity?> GetByIdAsync(Guid id);
}