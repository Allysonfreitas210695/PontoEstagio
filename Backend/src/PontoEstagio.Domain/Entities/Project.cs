using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;

namespace PontoEstagio.Domain.Entities;

public class Project : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
    public DateTime StartDate { get;  set; }
    public DateTime? EndDate { get;  set; } 
    public Guid CreatedBy { get; private set; }
    public User Creator { get; private set; } = default!;
    public ICollection<UserProject> UserProjects { get; private set; } = new List<UserProject>();
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();

    public Project() { }

    public Project(string name, string description, ProjectStatus status, DateTime startDate, DateTime? endDate, Guid createdBy)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
        CreatedBy = createdBy;
    }

    public void UpdateName(string name) => Name = name; 
    public void UpdateDescription(string description) => Description = description; 
    public void UpdateStatus(ProjectStatus status) => Status = status;
    public void UpdateStartDate(DateTime startDate) => StartDate = startDate;
    public void UpdateEndDate(DateTime? endDate) => EndDate = endDate;
    public void UpdateCreatedBy(Guid createdBy) => CreatedBy = createdBy;
}

