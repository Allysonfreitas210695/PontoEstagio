namespace PontoEstagio.Communication.Request;

public class RequestRegisterCompanytJson
{
    public string Name { get;  set; } = string.Empty;
    public string CNPJ { get;  set; } = string.Empty;
    public string Phone { get;  set; } = string.Empty;
    public string Email { get;  set; } = string.Empty;
    public bool IsActive { get;  set; } = true;
}
