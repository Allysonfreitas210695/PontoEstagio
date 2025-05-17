namespace PontoEstagio.Domain.Repositories.Attendance;

public interface IAttendanceUpdateOnlyRepository
{
    Task<Entities.Attendance?> GetByIdAsync(Guid id);
    void Update(Entities.Attendance attendance);
}