using Microsoft.Extensions.Configuration;
using PontoEstagio.Communication.Requests;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Helpers;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Domain.Services.Storage;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Activity.Create;

public class RegisterActivityUseCase : IRegisterActivityUseCase
{
    private readonly IActivityWriteOnlyRepository _activityWriteOnlyRepository;
    private readonly IAttendanceReadOnlyRepository _attendanceReadOnlyRepository;
    private readonly ILoggedUser _loggedUser; 
    private readonly IFileStorage _fileStorage;
    public RegisterActivityUseCase(
        IActivityWriteOnlyRepository activityWriteOnlyRepository,
        IAttendanceReadOnlyRepository attendanceReadOnlyRepository,
        ILoggedUser loggedUser,
        IFileStorage fileStorage
    )
    {
        _activityWriteOnlyRepository = activityWriteOnlyRepository;
        _attendanceReadOnlyRepository = attendanceReadOnlyRepository;
        _loggedUser = loggedUser; 
        _fileStorage = fileStorage;
    }

    public async Task<ResponseShortActivityJson> ExecuteAsync(RequestRegisterActivityJson request)
    {
        Validator(request);

        var _user = await _loggedUser.Get();
        if (_user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        var attendance = await _attendanceReadOnlyRepository.GetByIdAsync(request.AttendanceId);
        if (attendance is null)
            throw new Exception(ErrorMessages.AttendanceNotFound);
 
        string? proofFilePath = null;
        if (!string.IsNullOrWhiteSpace(request.ProofFilePath))
            proofFilePath = await _fileStorage.SaveBase64FileAsync(request.ProofFilePath);

        var activity = new Domain.Entities.Activity(
            id: null,
            attendanceId: attendance.Id,
            userId: _user!.Id,
            description: request.Description,
            recordedAt: DateTime.UtcNow,
            proofFilePath: proofFilePath
        );

        await _activityWriteOnlyRepository.AddAsync(activity);

        return new ResponseShortActivityJson
        {
            Id = activity.Id,
            Description = activity.Description,
            RecordedAt = activity.RecordedAt,
            CreatedAt = activity.CreatedAt,
            AttendanceId = activity.AttendanceId,
            ProofFilePath = activity.ProofFilePath
        };
    }

    private void Validator(RequestRegisterActivityJson request)
    {
        var result = new RegisterActivityValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
