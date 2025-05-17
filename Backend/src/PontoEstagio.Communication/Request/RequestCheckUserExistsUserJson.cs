using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Request;

public class RequestCheckUserExistsUserJson
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType Type { get; set; } = UserType.Intern;
}