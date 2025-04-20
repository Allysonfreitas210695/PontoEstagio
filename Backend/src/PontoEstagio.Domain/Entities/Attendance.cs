
using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.Domain.Entities;

public class Attendance : Entity
{
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan CheckIn { get; private set; }
    public TimeSpan CheckOut { get; private set; }
    public AttendanceStatus Status { get; private set; }

    public User User { get; private set; } = default!;
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();

    public Attendance() { }

    public Attendance(Guid userId, DateTime date, TimeSpan checkIn, TimeSpan checkOut, AttendanceStatus status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Date = date.Date;
        CheckIn = checkIn;
        CheckOut = checkOut;
        Status = status;
    }
}