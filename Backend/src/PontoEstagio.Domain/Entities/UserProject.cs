using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;

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

    public UserProject(Guid userId, Guid projectId, UserType role)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        ProjectId = projectId;
        AssignedAt = DateTime.UtcNow;
        IsCurrent = true;
        Role = role;
    }

    public void MarkAsInactive() => IsCurrent = false; 
}
