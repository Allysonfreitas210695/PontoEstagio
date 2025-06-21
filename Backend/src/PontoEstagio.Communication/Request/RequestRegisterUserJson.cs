using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Request;

public class RequestRegisterUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; 
    public string Registration { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string VerificationCode { get; set; } = string.Empty;
    public Guid UniversityId { get; set; }
    public Guid? CourseId { get; set; }
    public bool? isActive { get; set; }
    public UserType Type { get; set; } = UserType.Intern;
}