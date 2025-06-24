namespace PontoEstagio.Communication.Responses;
public class ResponseShortActivityJson
{
    public Guid Id { get; set; }
    public Guid AttendanceId { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime RecordedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ProofFilePath { get; set; }
}
