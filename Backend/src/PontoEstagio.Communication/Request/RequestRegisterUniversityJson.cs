namespace PontoEstagio.Communication.Request;

public class RequestRegisterUniversityJson
{
    public string Name { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public RequestAddressJson Address { get; set; } = default!;
}