using PontoEstagio.Domain.Common;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class Activity : Entity
{
    public Guid AttendanceId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ProjectId { get; private set; } 

    public string Description { get; private set; } = string.Empty;
    public DateTime RecordedAt { get; private set; }
    public string? ProofFilePath { get; private set; }

    public Attendance Attendance { get; private set; } = default!;
    public User User { get; private set; } = default!;
    public Project Project { get; private set; } = default!;
    
    protected Activity() { }

    public Activity(
        Guid? id,
        Guid attendanceId,
        Guid userId,
        Guid projectId,
        string description,
        DateTime recordedAt,
        string? proofFilePath = null
    )
    {
        Id = id ?? Guid.NewGuid();

        if (attendanceId == Guid.Empty)
            throw new ArgumentException(ErrorMessages.invalidAttendanceId);

        if (userId == Guid.Empty)
            throw new ArgumentException(ErrorMessages.invalidUserId);

        if (projectId == Guid.Empty)
            throw new ArgumentException(ErrorMessages.invalidProjectId);

        if (recordedAt > DateTime.Now)
            throw new ArgumentException(ErrorMessages.invalidRecordedAtDate);

        if (proofFilePath != null && string.IsNullOrWhiteSpace(proofFilePath))
            throw new ArgumentException(ErrorMessages.invalidProofFilePath);

        AttendanceId = attendanceId;
        UserId = userId;
        ProjectId = projectId;
        Description = description;
        RecordedAt = recordedAt;
        ProofFilePath = proofFilePath;
    }
}
