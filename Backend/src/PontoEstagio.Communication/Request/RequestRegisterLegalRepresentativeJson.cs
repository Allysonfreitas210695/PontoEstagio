namespace PontoEstagio.Communication.Request;

public class RequestRegisterLegalRepresentativeJson
{
    public string FullName { get;  set; } = string.Empty;
    public string CPF { get;  set; } = string.Empty;
    public string Position { get;  set; } = string.Empty;
    public string Email { get;  set; } = string.Empty;
    public required Guid CompanyId { get;  set; }
}