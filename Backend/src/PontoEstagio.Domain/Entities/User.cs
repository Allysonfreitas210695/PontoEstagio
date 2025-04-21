 using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; } = string.Empty;
    public Email Email { get;  private set; } = default!;
    public UserType Type { get; private set; } = UserType.Intern;
    public bool IsActive { get; private set; }
    public string Password { get;  private set; } = string.Empty;

    public ICollection<UserProject> UserProjects { get; private set; } = new List<UserProject>();
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();

    public User() { }

    public User(Guid? id, string name, Email email, UserType type, string password, bool isActive)
    {
        Id = id is null ? Guid.NewGuid() : id.Value;

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(ErrorMessages.invalidUserName);

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException(ErrorMessages.invalidPassword);

        if (!UserType.IsDefined(typeof(UserType), type))
            throw new ArgumentException(ErrorMessages.InvalidUserType);

        Name = name;
        Email = email;
        Type = type;
        Password = password;
        IsActive = isActive;
    }

    public void Inactivate() => IsActive = false; 
    public void Activate() => IsActive = true; 
    public void UpdateType(UserType type) => Type = type; 
    public void UpdateName(string name)  => Name = name; 
    public void UpdateEmail(string email) => Email = Email.Criar(email);
    public void UpdatePassword(string password) => Password = password;
}

