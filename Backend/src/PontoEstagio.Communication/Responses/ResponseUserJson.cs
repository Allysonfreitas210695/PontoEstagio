namespace PontoEstagio.Communication.Responses;
public class ResponseUserJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Type { get; set; } = string.Empty; 
}
