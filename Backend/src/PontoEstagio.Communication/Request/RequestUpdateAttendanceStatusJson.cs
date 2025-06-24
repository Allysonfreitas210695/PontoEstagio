using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Request;

public class RequestUpdateAttendanceStatusJson
{
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Pending;
}