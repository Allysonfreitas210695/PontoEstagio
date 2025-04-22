using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class Project : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
    public DateTime StartDate { get;  set; }
    public DateTime? EndDate { get;  set; } 
    public long TotalHours { get; set; } 
    public Guid CreatedBy { get; private set; }
    public User Creator { get; private set; } = default!;
    public Guid CompanyId { get; private set; } 
    public virtual Company Company { get; private set; }  = default!;
    public ICollection<UserProject> UserProjects { get; private set; } = new List<UserProject>();
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();

    public Project() { }

    public Project(Guid? id, Guid companyId, string name, string description, long totalHours, ProjectStatus status, DateTime startDate, DateTime? endDate, Guid createdBy)
    {
        Id = id ?? Guid.NewGuid();

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(ErrorMessages.invalidProjectName); 

        if (name.Length < 3)
            throw new ArgumentException(ErrorMessages.invalidProjectNameLength);

        if (companyId == Guid.Empty)
            throw new ArgumentException(ErrorMessages.InvalidCompanyId);

        Name = name;
        Description = description;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
        CreatedBy = createdBy;
        CompanyId = companyId;

        if (totalHours <= 0)
            throw new ArgumentException(ErrorMessages.invalidTotalHours);

        TotalHours = totalHours;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(ErrorMessages.invalidProjectName);

        if (name.Length < 3)
            throw new ArgumentException(ErrorMessages.invalidProjectNameLength);

        Name = name;
        UpdateTimestamp();
    } 

    public void UpdateDescription(string description)
    {
        Description = description;
        UpdateTimestamp();
    } 

    public void UpdateStatus(ProjectStatus status)
    {
        Status = status;
        UpdateTimestamp();
    }

    public void UpdateStartDate(DateTime startDate)
    {
        StartDate = startDate;
        UpdateTimestamp();
    }

    public void UpdateEndDate(DateTime? endDate)
    {
        EndDate = endDate;
        UpdateTimestamp();
    }

    public void UpdateCreatedBy(Guid createdBy)
    {
        CreatedBy = createdBy;
        UpdateTimestamp();
    }

    public void UpdateTotalHours(long totalHours) {
        if (totalHours <= 0)
            throw new ArgumentException(ErrorMessages.invalidTotalHours);
        TotalHours = totalHours;
        UpdateTimestamp();
    } 

    public void UpdateComapany_Id(Guid companyId)
    {
        CompanyId = companyId;
        UpdateTimestamp();
    }
}

