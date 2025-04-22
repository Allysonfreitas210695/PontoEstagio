namespace PontoEstagio.Communication.Responses;

public class ResponseCompanyJson
{
    public Guid Id { get;  set; }
    public string Name { get;  set; } = string.Empty;
    public string CNPJ { get;  set; } = string.Empty;
    public string Phone { get;  set; } = string.Empty;
    public string Email { get;  set; } = string.Empty;
    public bool IsActive { get;  set; } 
    public DateTime CreatedAt { get;  set; } 
}
