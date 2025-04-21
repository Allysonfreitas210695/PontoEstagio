using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;
public class UserProject : Entity
{
    public Guid UserId { get; private set; }
    public Guid ProjectId { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public bool IsCurrent { get; private set; }
    public UserType Role { get; private set; }

    public User User { get; private set; } = default!;
    public Project Project { get; private set; } = default!;

    protected UserProject() { }

    public UserProject(Guid? id, Guid userId, Guid projectId, UserType role)
    {
        Id = id ?? Guid.NewGuid();

        if (userId == Guid.Empty)
            throw new ArgumentException(ErrorMessages.invalidUserId);

        UserId = userId;

        if (projectId == Guid.Empty)
            throw new ArgumentException(ErrorMessages.invalidProjectId);

        ProjectId = projectId;
        AssignedAt = DateTime.UtcNow;
        IsCurrent = true;

        if (!UserType.IsDefined(typeof(UserType), role))
            throw new ArgumentException(ErrorMessages.InvalidUserType);

        Role = role;
    }

    public void MarkAsInactive() => IsCurrent = false; 
}
