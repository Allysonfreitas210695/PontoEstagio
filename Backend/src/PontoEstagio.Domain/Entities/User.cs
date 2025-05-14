 using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; } = string.Empty;
    public Email Email { get;  private set; } = default!;
    public UserType Type { get; private set; } = UserType.Intern;
    public bool IsActive { get; private set; }
    public string Registration { get; private set; }
    public string Password { get;  private set; } = string.Empty;
    public Guid UniversityId { get; private set; } 
    public virtual University University { get; private set; }  = default!;
    public ICollection<UserProject> UserProjects { get; private set; } = new List<UserProject>();
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();

    public User() { }

    public User(
            Guid? id, 
            Guid universityId,
            string name, 
            string registration, 
            Email email, 
            UserType type, 
            string password, 
            bool isActive
    )
    {
        Id = id is null ? Guid.NewGuid() : id.Value;

        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidUserName });
        
        if (universityId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUniversityId });

        if (string.IsNullOrWhiteSpace(password))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidPassword });

        if (!UserType.IsDefined(typeof(UserType), type))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUserType });

        Name = name;
        Email = email;
        Type = type;
        Password = password;
        IsActive = isActive;
        Registration = registration;
        UniversityId = universityId;
    }

    public void Inactivate() {
        IsActive = false;
        UpdateTimestamp();
    }
    
    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    public void UpdateType(UserType type)
    {
        if (!UserType.IsDefined(typeof(UserType), type))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUserType });

        Type = type;
        UpdateTimestamp();
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidUserName });

        Name = name;
        UpdateTimestamp();
    }

    public void UpdateEmail(string email)
    {
        Email = Email.Criar(email);
        UpdateTimestamp();
    }

    public void UpdatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidPassword });

        Password = password;
        UpdateTimestamp();
    }
}

