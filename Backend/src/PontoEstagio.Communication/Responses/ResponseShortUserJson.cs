using System.Diagnostics;

namespace PontoEstagio.Communication.Responses;
public class ResponseShortUserJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } 
}
