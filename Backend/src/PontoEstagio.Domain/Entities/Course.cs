using PontoEstagio.Domain.Common;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class Course : Entity
{
    public string Name { get; set; } = null!;
    public int WorkloadHours { get; set; } 
    public Guid UniversityId { get; set; } 
    public University University { get; set; } = default!;
    public ICollection<User> Users { get; set; } = new List<User>();
    
    public Course() { }

    public Course(
        Guid? id,
        string name, 
        int workloadHours, 
        Guid universityId
    )
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCourseName });
        
        if (universityId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUniversityId });

        if (workloadHours <= 0)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidWorkloadHours });


        Id = id ?? Guid.NewGuid();
        Name = name;
        WorkloadHours = workloadHours;
        UniversityId = universityId;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidCourseName });

        Name = name;
        UpdateTimestamp();
    }

    public void UpdateWorkloadHours(int workloadHours)
    {
        if (workloadHours <= 0)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidWorkloadHours });

        WorkloadHours = workloadHours;
        UpdateTimestamp();
    }

    public void UpdateUniversityId(Guid universityId)
    {
        if (universityId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUniversityId });

        UniversityId = universityId;
        UpdateTimestamp();
    }
}
