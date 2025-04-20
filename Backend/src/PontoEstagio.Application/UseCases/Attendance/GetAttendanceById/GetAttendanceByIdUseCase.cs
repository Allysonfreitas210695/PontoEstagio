using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Attendance.GetAttendanceById;

public class GetAttendanceByIdUseCase : IGetAttendanceByIdUseCase
{
    private readonly IAttendanceReadOnlyRepository _attendanceReadOnlyRepository;
    
    public GetAttendanceByIdUseCase(
        IAttendanceReadOnlyRepository attendanceReadOnlyRepository
    )
    {
        _attendanceReadOnlyRepository = attendanceReadOnlyRepository;
    }

    public async Task<ResponseAttendanceJson> Execute(Guid id)
    {
        var _attendance = await _attendanceReadOnlyRepository.GetByIdAsync(id);

        if (_attendance == null)
            throw new NotFoundException("Attendance not found");

        return new ResponseAttendanceJson() {
            Id = _attendance.Id,
            UserId = _attendance.UserId,
            Date = _attendance.Date,
            CheckIn = _attendance.CheckIn,
            CheckOut = _attendance.CheckOut,
            Status = _attendance.Status.ToString(),
            Activities = _attendance.Activities.Select(a => new ResponseActivityJson()
            {
                Id = a.Id,
                UserId = a.Id,
                Description = a.Description,
                AttendanceId = a.AttendanceId,
                ProjectId = a.ProjectId,
                ProofFilePath = a.ProofFilePath,
                RecordedAt = a.RecordedAt,
            }).ToList()
        };
    }
}