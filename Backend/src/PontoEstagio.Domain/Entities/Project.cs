using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.Exceptions;
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
    public Guid UniversityId { get; private set; } 
    public virtual University University { get; private set; }  = default!;
    public ICollection<UserProject> UserProjects { get; private set; } = new List<UserProject>();
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();

    public Project() { }

    public Project(
        Guid? id, 
        Guid companyId, 
        Guid universityId,
        string name, 
        string description, 
        long totalHours, 
        ProjectStatus status, 
        DateTime startDate, 
        DateTime? endDate,
        Guid createdBy
)
    {
        Id = id ?? Guid.NewGuid();

        if (string.IsNullOrEmpty(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidProjectName });

        if (name.Length < 3)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidProjectNameLength });

        if (companyId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCompanyId });

        if (universityId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUniversityId });

        Name = name;
        Description = description;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
        CreatedBy = createdBy;
        CompanyId = companyId;
        UniversityId = universityId;

        if (totalHours <= 0)
            throw new ArgumentException(ErrorMessages.InvalidTotalHours);

        TotalHours = totalHours;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(ErrorMessages.InvalidProjectName);

        if (name.Length < 3)
            throw new ArgumentException(ErrorMessages.InvalidProjectNameLength);

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
            throw new ArgumentException(ErrorMessages.InvalidTotalHours);
        TotalHours = totalHours;
        UpdateTimestamp();
    } 

    public void UpdateComapany_Id(Guid companyId)
    {
        CompanyId = companyId;
        UpdateTimestamp();
    }
}

