using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Attendance.UpdateStatus;

public interface IUpdateAttendanceStatusUseCase
{
    Task Execute(Guid attendanceId, RequestUpdateAttendanceStatusJson request);
}