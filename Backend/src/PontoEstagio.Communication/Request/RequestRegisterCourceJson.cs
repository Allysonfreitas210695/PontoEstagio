namespace PontoEstagio.Communication.Request;

public class RequestRegisterCourceJson
{
    public string Name { get; set; } = null!;
    public int WorkloadHours { get; set; } 
    public Guid UniversityId { get; set; } 
}