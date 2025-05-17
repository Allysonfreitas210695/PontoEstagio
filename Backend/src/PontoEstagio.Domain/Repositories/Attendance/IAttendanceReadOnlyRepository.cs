namespace PontoEstagio.Domain.Repositories;

public interface IAttendanceReadOnlyRepository
{
    Task<Entities.Attendance?> GetByIdAsync(Guid id);
    Task<List<Entities.Attendance>> GetAllByInternAsync(Guid intern_id);
    Task<List<Entities.Attendance>> GetAllBySupervisorAsync(Guid supervisor_id);
    Task<Entities.Attendance?> GetByUserIdAndDateAsync(Guid userId, DateTime date);
}