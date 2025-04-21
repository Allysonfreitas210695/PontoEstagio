using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;                 
using PontoEstagio.Domain.Repositories.UserProjects; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Projects.AssignUserToProject;

public class AssignUserToProjectUseCase : IAssignUserToProjectUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProjectsReadOnlyRepository _userProjectsReadOnlyRepository;
    private readonly IUserProjectsWriteOnlyRepository _userProjectsWriteOnlyRepository;

    public AssignUserToProjectUseCase(
        ILoggedUser loggedUser,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IUserProjectsReadOnlyRepository userProjectsReadOnlyRepository,
        IUserProjectsWriteOnlyRepository userProjectsWriteOnlyRepository
    )
    {
        _loggedUser = loggedUser;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _userProjectsReadOnlyRepository = userProjectsReadOnlyRepository;
        _userProjectsWriteOnlyRepository = userProjectsWriteOnlyRepository;
    }

    public async Task Execute(Guid projectId, RequestAssignUserToProjectJson request)
    {
        var supervisor = await _loggedUser.Get();

        if (supervisor is null || supervisor.Type != UserType.Supervisor)
            throw new ForbiddenException(ErrorMessages.SupervisorNotFoundOrNotSupervisor);

        var intern = await _userReadOnlyRepository.GetUserByIdAsync(request.User_Id);
        if (intern is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (intern.Type != UserType.Intern)
            throw new ForbiddenException(ErrorMessages.UserNotIntern);

        var alreadyAssignedToProject = await _userProjectsReadOnlyRepository
            .ExistsProjectAssignedToUserAsync(projectId, intern.Id);

        if (alreadyAssignedToProject)
            throw new NotFoundException(ErrorMessages.UserAlreadyAssignedToProject);

        var existingProject = await _userProjectsReadOnlyRepository
            .GetCurrentProjectForUserAsync(intern.Id);

        if (existingProject != null)
            throw new NotFoundException(ErrorMessages.UserAlreadyHasOngoingProject);

        var supervisorAssigned = await _userProjectsReadOnlyRepository
            .ExistsProjectAssignedToUserAsync(projectId, supervisor.Id);

        if (supervisorAssigned)
            throw new NotFoundException(ErrorMessages.SupervisorAlreadyAssignedToProject);

        var supervisorProject = new UserProject(Guid.NewGuid(), supervisor.Id, projectId, supervisor.Type);
        var internProject = new UserProject(Guid.NewGuid(), intern.Id, projectId, intern.Type);

        await _userProjectsWriteOnlyRepository.AddUserToProjectAsync(supervisorProject);
        await _userProjectsWriteOnlyRepository.AddUserToProjectAsync(internProject);

        await _unitOfWork.CommitAsync();
    }
}
