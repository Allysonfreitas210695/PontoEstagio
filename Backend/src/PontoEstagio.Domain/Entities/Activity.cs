using PontoEstagio.Domain.Common;

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
        Guid attendanceId,
        Guid userId,
        Guid projectId,
        string description,
        DateTime recordedAt,
        string? proofFilePath = null
    )
    {
        Id = Guid.NewGuid();
        AttendanceId = attendanceId;
        UserId = userId;
        ProjectId = projectId;
        Description = description;
        RecordedAt = recordedAt;
        ProofFilePath = proofFilePath;
    }
}
