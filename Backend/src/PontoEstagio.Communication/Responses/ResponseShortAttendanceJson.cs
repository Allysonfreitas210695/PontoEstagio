namespace PontoEstagio.Communication.Responses;

public class ResponseShortAttendanceJson
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan CheckIn { get; set; }
    public TimeSpan CheckOut { get; set; }
    public string Status { get; set; } = string.Empty;
}