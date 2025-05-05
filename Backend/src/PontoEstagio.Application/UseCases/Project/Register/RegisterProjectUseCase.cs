using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Projects; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Projects.Register;
public class RegisterProjectUseCase : IRegisterProjectUseCase
{
    private readonly IProjectWriteOnlyRepository _projectWriteOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterProjectUseCase(
        IProjectWriteOnlyRepository projectWriteOnlyRepository,
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork
    )
    {
        _projectWriteOnlyRepository = projectWriteOnlyRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseShortProjectJson> Execute(RequestRegisterProjectJson request)
    {
        Validate(request);

        var user = await _loggedUser.Get();

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.Type != UserType.Admin)
            throw new ForbiddenException(ErrorMessages.UserNotAdmin);

        var _project = new Domain.Entities.Project(
            Guid.NewGuid(),
            request.CompanyId,
            request.Name, 
            request.Description, 
            request.TotalHours,
            ProjectStatus.Planning, 
            request.StartDate,
            request.EndDate,
            user.Id
        );

        await _projectWriteOnlyRepository.AddAsync(_project);

        await _unitOfWork.CommitAsync();

        return new ResponseShortProjectJson()
        {
            Id = _project.Id,
            Description = _project.Description,
            Name = _project.Name,
            Status = _project.Status.ToString(),
            StartDate = _project.StartDate,
            EndDate = _project.EndDate,
            CreatedAt = _project.CreatedAt
        };
    }

    private void Validate(RequestRegisterProjectJson request)
    {
        var result = new RegisterProjectValidator().Validate(request);
 
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
