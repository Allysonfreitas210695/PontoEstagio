
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Activity.GetActivitiesByAttendanceId;

public class GetActivitiesByAttendanceIdUseCase : IGetActivitiesByAttendanceIdUseCase
{
    private readonly IActivityReadOnlyRepository _activityReadOnlyRepository;

    public GetActivitiesByAttendanceIdUseCase(IActivityReadOnlyRepository activityReadOnlyRepository)
    {
        _activityReadOnlyRepository = activityReadOnlyRepository;
    }

    public async Task<List<ResponseActivityJson>> Execute(Guid attendanceId)
    {
        var activities = await _activityReadOnlyRepository.GetByAttendanceIdAsync(attendanceId);

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