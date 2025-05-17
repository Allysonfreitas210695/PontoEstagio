using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

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
            throw new NotFoundException(ErrorMessages.AttendanceNotFound);

        return new ResponseAttendanceJson() {
            Id = _attendance.Id,
            UserId = _attendance.UserId,
            Date = _attendance.Date,
            CheckIn = _attendance.CheckIn,
            CheckOut = _attendance.CheckOut,
            Status = _attendance.Status.ToString(),
            CreatedAt = _attendance.CreatedAt,
            //Activities = _attendance.Activities.Select(a => new ResponseActivityJson()
            //{
            //    Id = a.Id,
            //    UserId = a.Id,
            //    Description = a.Description,
            //    Attendance = new ResponseAttendanceJson()
            //    {
            //        Id = a.Attendance.Id,
            //        UserId = a.Attendance.UserId,
            //        CheckIn = a.Attendance.CheckIn,
            //        CheckOut = a.Attendance.CheckOut,
            //        Date = a.Attendance.Date,
            //        Status = a.Attendance.Status.ToString(),
            //    },
            //    ProofFilePath = a.ProofFilePath,
            //    RecordedAt = a.RecordedAt,
            //}).ToList()
        };
    }
}