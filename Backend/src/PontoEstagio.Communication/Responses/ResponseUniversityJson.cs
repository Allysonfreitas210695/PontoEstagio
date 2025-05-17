namespace PontoEstagio.Communication.Responses;

public class ResponseUniversityJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ResponseAddressJson Address { get; set; } = default!;
}