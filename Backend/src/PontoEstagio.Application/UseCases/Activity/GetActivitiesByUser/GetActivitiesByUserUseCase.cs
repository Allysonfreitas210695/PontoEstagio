using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Activity.GetActivitiesByUser;

public class GetActivitiesByUserUseCase : IGetActivitiesByUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IActivityReadOnlyRepository _activityReadOnlyRepository;

    public GetActivitiesByUserUseCase(
        ILoggedUser loggedUser, 
        IActivityReadOnlyRepository activityReadOnlyRepository
    )
    {
        _loggedUser = loggedUser;
        _activityReadOnlyRepository = activityReadOnlyRepository;
    }

    public async Task<List<ResponseActivityJson>> Execute(Guid userId)
    {
        var user = await _loggedUser.Get();
        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (!user.IsActive)
            throw new ForbiddenException(ErrorMessages.UserNotActive);

        var activities = user.Type switch
        {
            UserType.Intern => await _activityReadOnlyRepository.GetByInternIdAsync(userId),
            UserType.Supervisor => await _activityReadOnlyRepository.GetBySupervisorIdAsync(userId),
            _ => throw new BusinessRuleException(ErrorMessages.InvalidUserTypeForListing)
        };

        return activities.Select(activity => new ResponseActivityJson
        {
            Id = activity.Id,
            Description = activity.Description,
            UserId = activity.UserId,
            RecordedAt = activity.RecordedAt,
            ProofFilePath = activity.ProofFilePath,
            Project =  new ResponseProjectJson
            {
                Id = activity.Project.Id,
                Name = activity.Project.Name,
                Description = activity.Project.Description,
                StartDate = activity.Project.StartDate,
                EndDate = activity.Project.EndDate,
                Status = activity.Project.Status.ToString()
            },
            Attendance = new ResponseAttendanceJson
            {
                Id = activity.Attendance.Id,
                UserId = activity.Attendance.UserId,
                CheckIn = activity.Attendance.CheckIn,
                CheckOut = activity.Attendance.CheckOut,
                Date = activity.Attendance.Date,
                Status = activity.Attendance.Status.ToString()
            }
        }).ToList();
    }
}