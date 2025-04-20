namespace PontoEstagio.Domain.Repositories;

public interface IAttendanceReadOnlyRepository
{
    Task<Entities.Attendance?> GetByUserIdAndDateAsync(Guid userId, DateTime date);
}