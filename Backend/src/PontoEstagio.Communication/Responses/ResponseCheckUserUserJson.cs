using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Responses;

public class ResponseCheckUserUserJson
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType Type { get; set; } = default!;
}