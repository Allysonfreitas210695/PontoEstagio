namespace PontoEstagio.Communication.Responses;

public class ResponseCourceJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int WorkloadHours { get; set; } 
    public ResponseUniversityJson University { get; set; } = default!;
    public DateTime CreatedAt { get;  set; }
}