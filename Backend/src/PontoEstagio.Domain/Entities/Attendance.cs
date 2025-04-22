
using System;
using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class Attendance : Entity
{
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan CheckIn { get; private set; }
    public TimeSpan CheckOut { get; private set; }
    public AttendanceStatus Status { get; set; }

    public User User { get; private set; } = default!;
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();

    public Attendance() { }

    public Attendance(Guid? id, Guid userId, DateTime date, TimeSpan checkIn, TimeSpan checkOut, AttendanceStatus status)
    {
        Id = id ?? Guid.NewGuid();

        if (userId == Guid.Empty)
            throw new ArgumentException(ErrorMessages.invalidUserId);

        if (date > DateTime.Now.Date)
            throw new ArgumentException(ErrorMessages.invalidAttendanceDate);

        if (checkOut <= checkIn)
            throw new ArgumentException(ErrorMessages.invalidCheckOutTime);

        UserId = userId;
        Date = date.Date;
        CheckIn = checkIn;
        CheckOut = checkOut;
        Status = status;
    }

    public void UpdateStatus(AttendanceStatus status) {
        Status = status;
        UpdateTimestamp();
    }
}