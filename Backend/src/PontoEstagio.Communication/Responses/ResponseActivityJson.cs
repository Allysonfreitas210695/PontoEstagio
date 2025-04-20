namespace PontoEstagio.Communication.Responses;

public class ResponseActivityJson
{
    public Guid Id { get;  set; }
    public Guid AttendanceId { get;  set; }
    public Guid UserId { get;  set; }
    public Guid ProjectId { get;  set; }

    public string Description { get;  set; } = string.Empty;
    public DateTime RecordedAt { get;  set; }
    public string? ProofFilePath { get;  set; } 
}
