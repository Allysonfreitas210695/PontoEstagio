using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Request;
public class RequestRegisterProjectJson
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
    public long TotalHours { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
