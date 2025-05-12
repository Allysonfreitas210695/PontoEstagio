using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Attendance; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Attendance.UpdateStatus;

public class UpdateAttendanceStatusUseCase : IUpdateAttendanceStatusUseCase
{
    private readonly IAttendanceUpdateOnlyRepository _attendanceUpdateOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAttendanceStatusUseCase(
        IAttendanceUpdateOnlyRepository attendanceUpdateOnlyRepository,
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork
    )
    {
        _attendanceUpdateOnlyRepository = attendanceUpdateOnlyRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid attendanceId, RequestUpdateAttendanceStatusJson request)
    {
        if (!Enum.IsDefined(typeof(Communication.Enum.AttendanceStatus), request.Status))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidStatus });

        var user = await _loggedUser.Get();

        if (user == null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.Type != UserType.Supervisor)
            throw new ForbiddenException(ErrorMessages.InvalidUserType);

        var _attendance = await _attendanceUpdateOnlyRepository.GetByIdAsync(attendanceId);
        if (_attendance == null)
            throw new NotFoundException(ErrorMessages.AttendanceNotFound);
        
        //if (!_attendance.Activities.Any(x => 
        //                                    x.Project != null && 
        //                                    x.Project.UserProjects.Any(y => y.UserId == user.Id))
        //                            )
        //    throw new BusinessRuleException(ErrorMessages.AttendanceMustHaveLinkedActivity);

        _attendance.UpdateStatus((AttendanceStatus)request.Status);
        _attendanceUpdateOnlyRepository.Update(_attendance);
        
        await _unitOfWork.CommitAsync();
    }
}