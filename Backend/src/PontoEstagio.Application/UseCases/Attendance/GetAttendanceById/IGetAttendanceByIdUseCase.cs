using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Attendance.GetAttendanceById;

public interface IGetAttendanceByIdUseCase
{
    Task<ResponseAttendanceJson> Execute(Guid id);
}