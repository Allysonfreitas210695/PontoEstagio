
using System;
using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class Attendance : Entity
{
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan CheckIn { get; private set; }
    public TimeSpan CheckOut { get; private set; }
    public AttendanceStatus Status { get; set; }
    public string ProofImageBase64 { get; private set; }

    public User User { get; private set; } = default!;
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();

    public Attendance() { }

    public Attendance(Guid? id, Guid userId, DateTime date, TimeSpan checkIn, TimeSpan checkOut, AttendanceStatus status, string proofImageBase64)
    {
        Id = id ?? Guid.NewGuid();

        if (userId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidUserId });
        
        if (date > DateTime.Now.Date)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidAttendanceDate });

        if (checkOut <= checkIn)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidCheckOutTime });

        UserId = userId;
        Date = date.Date;
        CheckIn = checkIn;
        CheckOut = checkOut;
        Status = status;

        AddProofImageBase64(proofImageBase64);
    }


    public void AddProofImageBase64(string base64Image)
    {
        if (string.IsNullOrWhiteSpace(base64Image))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidProofImage });

        ProofImageBase64 = base64Image;
        UpdateTimestamp();
    }

    public void UpdateStatus(AttendanceStatus status) {
        Status = status;
        UpdateTimestamp();
    }
}