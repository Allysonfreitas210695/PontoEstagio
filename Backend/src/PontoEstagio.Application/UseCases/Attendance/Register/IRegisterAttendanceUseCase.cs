using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Attendance.Register;

public interface IRegisterAttendanceUseCase
{
    Task<ResponseShortAttendanceJson> Execute(RequestRegisterAttendanceJson request); 
}