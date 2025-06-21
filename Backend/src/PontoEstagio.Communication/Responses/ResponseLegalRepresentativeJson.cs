namespace PontoEstagio.Communication.Responses;

public class ResponseLegalRepresentativeJson
{
    public Guid Id { get; set; }
    public string FullName { get;  set; } = string.Empty;
    public string CPF { get;  set; } = string.Empty;
    public string Position { get;  set; } = string.Empty;
    public string Email { get;  set; } = string.Empty;
    public ResponseCompanyJson Company { get;  set; } = new ResponseCompanyJson();
}