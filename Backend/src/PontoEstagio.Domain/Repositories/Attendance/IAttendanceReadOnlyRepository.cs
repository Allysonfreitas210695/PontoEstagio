namespace PontoEstagio.Domain.Repositories;

public interface IAttendanceReadOnlyRepository
{
    Task<List<Entities.Attendance>> GetAllByInternAsync(Guid intern_id);
    Task<Entities.Attendance?> GetByUserIdAndDateAsync(Guid userId, DateTime date);
}