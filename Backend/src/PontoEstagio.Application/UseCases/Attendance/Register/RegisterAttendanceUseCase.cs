using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Attendance;
using PontoEstagio.Domain.Repositories.UserProjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Attendance.Register;

public class RegisterAttendanceUseCase : IRegisterAttendanceUseCase
{
    private readonly IAttendanceReadOnlyRepository _attendanceReadOnlyRepository;
    private readonly IAttendanceWriteOnlyRepository _attendanceWriteOnlyRepository;
    private readonly IUserProjectsReadOnlyRepository _userProjectsReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterAttendanceUseCase(
        IAttendanceReadOnlyRepository attendanceReadOnlyRepository,
        IAttendanceWriteOnlyRepository attendanceWriteOnlyRepository,
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork,
        IUserProjectsReadOnlyRepository userProjectsReadOnlyRepository
    )
    {
        _attendanceReadOnlyRepository = attendanceReadOnlyRepository;
        _attendanceWriteOnlyRepository = attendanceWriteOnlyRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _userProjectsReadOnlyRepository = userProjectsReadOnlyRepository;
    }

    public async Task<ResponseShortAttendanceJson> Execute(RequestRegisterAttendanceJson request)
    {
        if (!Enum.IsDefined(typeof(Communication.Enum.AttendanceStatus), request.Status))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidStatus }); 

        Validator(request);

        var _user = await _loggedUser.Get();
        if(_user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (_user.Type != UserType.Intern)
            throw new ForbiddenException(ErrorMessages.InvalidUserType);

       var existingProject = _userProjectsReadOnlyRepository.GetCurrentProjectForUserAsync(_user.Id);
        if (existingProject != null)
            throw new NotFoundException(ErrorMessages.UserAlreadyHasOngoingProject);

        var existingAttendance = await _attendanceReadOnlyRepository
                .GetByUserIdAndDateAsync(_user.Id, request.Date);
        if (existingAttendance != null)
            throw new BusinessRuleException(ErrorMessages.AttendanceAlreadyExists);

        //TODO: salvar imagem no servidor

        var newAttendance = new Domain.Entities.Attendance(
            Guid.NewGuid(),
            _user.Id,
            request.Date,
            request.CheckIn,
            request.CheckOut,
            (AttendanceStatus)request.Status,
            request.ProofImageBase64
        );

        await _attendanceWriteOnlyRepository.AddAsync(newAttendance);
        await _unitOfWork.CommitAsync();

        return new ResponseShortAttendanceJson
        {
            Id = newAttendance.Id,
            Status = newAttendance.Status.ToString(),
            UserId = newAttendance.UserId,
            Date = newAttendance.Date,
            CheckIn = newAttendance.CheckIn,
            CheckOut = newAttendance.CheckOut,
            CreatedAt = newAttendance.CreatedAt,
        };
    }

    private void Validator(RequestRegisterAttendanceJson request)
    {
        var result = new RegisterAttendanceValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}