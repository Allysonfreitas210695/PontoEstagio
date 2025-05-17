using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Attendance.GetAllAttendances;

public interface IGetAllAttendancesUseCase
{
    Task<List<ResponseAttendanceJson>> Execute();
}