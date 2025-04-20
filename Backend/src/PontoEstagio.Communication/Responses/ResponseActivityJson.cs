namespace PontoEstagio.Communication.Responses;

public class ResponseActivityJson
{
    public Guid Id { get;  set; }
    public ResponseAttendanceJson Attendance { get; set; } = default!;
    public ResponseProjectJson Project { get; set; } = default!;
    public Guid UserId { get;  set; } 
    public string Description { get;  set; } = string.Empty;
    public DateTime RecordedAt { get;  set; }
    public string? ProofFilePath { get;  set; } 
}
