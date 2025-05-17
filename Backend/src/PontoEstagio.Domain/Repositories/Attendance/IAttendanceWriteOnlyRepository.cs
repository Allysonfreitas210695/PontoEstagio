namespace PontoEstagio.Domain.Repositories.Attendance;

public interface IAttendanceWriteOnlyRepository
{
    Task AddAsync(Entities.Attendance attendance);
}