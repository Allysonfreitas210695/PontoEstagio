using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Activity; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Activity.GetActivityById;

public class GetActivityByIdUseCase : IGetActivityByIdUseCase
{
    private readonly IActivityReadOnlyRepository _activityReadOnlyRepository;

    public GetActivityByIdUseCase(IActivityReadOnlyRepository activityReadOnlyRepository)
    {
        _activityReadOnlyRepository = activityReadOnlyRepository;
    }

    public async Task<ResponseActivityJson> Execute(Guid id)
    {
        var activity = await _activityReadOnlyRepository.GetByIdAsync(id);

        if (activity is null)
            throw new NotFoundException(ErrorMessages.ActivityNotFound);
            
        return new ResponseActivityJson() {
            Id = activity.Id,
            Description = activity.Description,
            Project = new ResponseProjectJson() {
                Id = activity.Project.Id,
                Name = activity.Project.Name
            },
            UserId = activity.UserId,
            Attendance = new ResponseAttendanceJson() {
                Id = activity.Attendance.Id,
                UserId = activity.Attendance.UserId,
                CheckIn = activity.Attendance.CheckIn,
                CheckOut = activity.Attendance.CheckOut,
                Date = activity.Attendance.Date,
                Status = activity.Attendance.Status.ToString(),
            }, 
            ProofFilePath = activity.ProofFilePath,
            RecordedAt = activity.RecordedAt
        };
    }
}