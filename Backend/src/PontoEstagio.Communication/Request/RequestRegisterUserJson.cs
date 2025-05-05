using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Request;

public class RequestRegisterUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; 
    public bool? isActive { get; set; }
    public UserType Type { get; set; } = UserType.Intern;
}