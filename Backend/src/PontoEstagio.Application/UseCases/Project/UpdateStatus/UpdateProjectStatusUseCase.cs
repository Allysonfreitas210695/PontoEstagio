
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Application.UseCases.Projects.UpdateStatus;
public class UpdateProjectStatusUseCase : IUpdateProjectStatusUseCase
{
    private readonly IProjectUpdateOnlyRepository _projectUpdateOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectStatusUseCase(
        IProjectUpdateOnlyRepository projectUpdateOnlyRepository,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork
    )
    {
        _projectUpdateOnlyRepository = projectUpdateOnlyRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid projectId, ProjectStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(ProjectStatus), newStatus))
            throw new ErrorOnValidationException(new List<string> { "Status is invalid." }); 

        var user = await _loggedUser.Get();

        if (user is null)
            throw new NotFoundException("user not exists.");

        if (user.Type != Domain.Enum.UserType.Supervisor)
            throw new ForbiddenException();

        var _project = await _projectUpdateOnlyRepository.GetProjectByIdAsync(projectId);
        if (_project is null)
            throw new NotFoundException("Project not found.");

        if (_project.CreatedBy != user.Id && user.Type != Domain.Enum.UserType.Supervisor)
            throw new ForbiddenException();

        _project.UpdateStatus((Domain.Enum.ProjectStatus)newStatus); 

        _projectUpdateOnlyRepository.Update(_project);

        await _unitOfWork.CommitAsync();
    }
 
}
