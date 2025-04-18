using System;
using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;

namespace PontoEstagio.Domain.Entities;

public class User : Entity
{
    public string Name { get;  set; } = string.Empty;
    public Email Email { get;  set; } = default!;
    public UserType Type { get;  set; } = UserType.Intern;
    public bool IsActive { get;  set; }
    public string Password { get;  set; } = string.Empty;

    public ICollection<UserProject> UserProjects { get; private set; } = new List<UserProject>();
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();

    public User() { }

    public User(Guid? id, string name, Email email, UserType type, string password, bool isActive)
    {
        Id = id is null ? Guid.NewGuid() : id.Value;
        Name = name;
        Email = email;
        Type = type;
        Password = password;
        IsActive = isActive;
    }
}

