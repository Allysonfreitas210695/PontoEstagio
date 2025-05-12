using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class Activity : Entity
{
    public Guid AttendanceId { get; private set; }
    public Guid UserId { get; private set; }

    public string Description { get; private set; } = string.Empty;
    public DateTime RecordedAt { get; private set; }
    public string? ProofFilePath { get; private set; }
    public ActivityStatus Status { get; private set; }

    public Attendance Attendance { get; private set; } = default!;

    public User User { get; private set; } = default!;
    
    protected Activity() { }

    public Activity(
        Guid? id,
        Guid attendanceId,
        Guid userId,
        string description,
        DateTime recordedAt,
        string? proofFilePath = null
    )
    {
        Id = id ?? Guid.NewGuid();

        if (attendanceId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidAttendanceId });

        if (userId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidUserId });

        if (recordedAt > DateTime.Now)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidRecordedAtDate });

        if (proofFilePath != null && string.IsNullOrWhiteSpace(proofFilePath))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidProofFilePath });

        AttendanceId = attendanceId;
        UserId = userId;
        Description = description;
        RecordedAt = recordedAt;
        ProofFilePath = proofFilePath;
        Status = ActivityStatus.Pending;
    }
}
