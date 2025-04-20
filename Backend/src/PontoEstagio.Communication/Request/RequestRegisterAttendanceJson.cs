using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Request;

public class RequestRegisterAttendanceJson
{
    public DateTime Date { get; set; }
    public TimeSpan CheckIn { get; set; }
    public TimeSpan CheckOut { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Pending;
}