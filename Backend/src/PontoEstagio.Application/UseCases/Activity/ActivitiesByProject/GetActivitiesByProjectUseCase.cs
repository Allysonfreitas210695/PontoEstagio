using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Domain.Repositories.Projects; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Activity.ActivitiesByProject;

public class GetActivitiesByProjectUseCase : IGetActivitiesByProjectUseCase
{
    protected readonly IActivityReadOnlyRepository _activityReadOnlyRepository;
    private readonly IProjectReadOnlyRepository _projectReadOnlyRepository;

    public GetActivitiesByProjectUseCase( 
        IActivityReadOnlyRepository activityReadOnlyRepository,
        IProjectReadOnlyRepository projectReadOnlyRepository
    )
    { 
        _activityReadOnlyRepository = activityReadOnlyRepository;
        _projectReadOnlyRepository = projectReadOnlyRepository;
    }

    public async Task<List<ResponseActivityJson>> Execute(Guid projectId)
    {
        var project = await _projectReadOnlyRepository.GetProjectByIdAsync(projectId);
        if (project is null) 
            throw new NotFoundException(ErrorMessages.ProjectNotFound);

        var activities = await _activityReadOnlyRepository.GetByProjectIdAsync(projectId);
        
        return activities.Select(activity => new ResponseActivityJson
        {
            Id = activity.Id,
            Description = activity.Description,
            Project = new ResponseProjectJson
            {
                Id = activity.Project.Id,
                Name = activity.Project.Name
            },
            UserId = activity.UserId,
            Attendance = new ResponseAttendanceJson
            {
                Id = activity.Attendance.Id,
                UserId = activity.Attendance.UserId,
                CheckIn = activity.Attendance.CheckIn,
                CheckOut = activity.Attendance.CheckOut,
                Date = activity.Attendance.Date,
                Status = activity.Attendance.Status.ToString(),
            },
            ProofFilePath = activity.ProofFilePath,
            RecordedAt = activity.RecordedAt
        }).ToList();
    }
}