namespace PontoEstagio.Communication.Responses;

public class ResponseShortProjectJson
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
}