using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Activity.GetActivitiesByAttendanceId;

public interface IGetActivitiesByAttendanceIdUseCase
{
    Task<List<ResponseActivityJson>> Execute(Guid attendanceId);
}