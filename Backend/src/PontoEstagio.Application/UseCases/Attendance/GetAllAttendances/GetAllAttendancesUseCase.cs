using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Attendance.GetAllAttendances;

public class GetAllAttendancesUseCase : IGetAllAttendancesUseCase
{
    private readonly IAttendanceReadOnlyRepository _attendanceReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;

    public GetAllAttendancesUseCase(
        IAttendanceReadOnlyRepository attendanceReadOnlyRepository,
        ILoggedUser loggedUser
    )
    {
        _attendanceReadOnlyRepository = attendanceReadOnlyRepository;
        _loggedUser = loggedUser;
    }

    public async Task<List<ResponseAttendanceJson>> Execute()
    {
        var _user = await _loggedUser.Get();

         if(_user is null)
            throw new NotFoundException("user not exists.");

        if(_user.Type != Domain.Enum.UserType.Intern)
            throw new ForbiddenException();

        var _attendances = await _attendanceReadOnlyRepository.GetAllByInternAsync(_user.Id);
        
        return _attendances.Select(z => new ResponseAttendanceJson
        {
            Id = z.Id,
            UserId = z.UserId,
            Date = z.Date,
            CheckIn = z.CheckIn,
            CheckOut = z.CheckOut,
            Status = z.Status.ToString(),
        }).ToList();
    }
}   