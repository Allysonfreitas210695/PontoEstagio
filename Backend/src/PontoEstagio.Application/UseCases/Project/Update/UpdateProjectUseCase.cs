using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Projects.Update;
public class UpdateProjectUseCase : IUpdateProjectUseCase
{
    private readonly IProjectUpdateOnlyRepository _projectUpdateOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectUseCase(
        IProjectUpdateOnlyRepository projectUpdateOnlyRepository,
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork
    )
    {
        _projectUpdateOnlyRepository = projectUpdateOnlyRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid projectId, RequestRegisterProjectJson request)
    {
        Validate(request);

        var user = await _loggedUser.Get();

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.Type != UserType.Admin)
            throw new ForbiddenException(ErrorMessages.UserNotAdmin);

        var _project = await _projectUpdateOnlyRepository.GetProjectByIdAsync(projectId);
        if (_project is null)
            throw new NotFoundException(ErrorMessages.ProjectNotFound);

        if (_project.CreatedBy != user.Id || user.Type != UserType.Admin)
            throw new ForbiddenException(ErrorMessages.UserNotAdmin);

        _project.UpdateName(request.Name);

        _project.UpdateStatus((ProjectStatus)request.Status);

        _project.UpdateDescription(request.Description);

        _project.UpdateStartDate(request.StartDate);

        _project.UpdateEndDate(request.EndDate);

        _project.UpdateComapany_Id(request.CompanyId);

        _projectUpdateOnlyRepository.Update(_project);

        await _unitOfWork.CommitAsync(); 
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
