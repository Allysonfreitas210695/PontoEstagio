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
        var admin = await _loggedUser.Get(); 

        if (admin is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (admin.Type != UserType.Admin)
            throw new ForbiddenException(ErrorMessages.UserNotAdmin);

        var intern = await _userReadOnlyRepository.GetUserByIdAsync(request.Intern_Id);
        if (intern is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (intern.Type != UserType.Intern)
            throw new ForbiddenException(ErrorMessages.UserNotIntern);

        var alreadyAssignedToProject = await _userProjectsReadOnlyRepository
            .ExistsProjectAssignedToUserAsync(projectId, intern.Id);
        if (alreadyAssignedToProject)
            throw new NotFoundException(ErrorMessages.UserAlreadyAssignedToProject);

        var existingProject = await _userProjectsReadOnlyRepository.GetCurrentProjectForUserAsync(intern.Id);
        if (existingProject != null && existingProject.Status == ProjectStatus.InProgress)
            throw new BusinessRuleException(ErrorMessages.InternAlreadyAssignedToActiveProject);

        var supervisor = await _userReadOnlyRepository.GetUserByIdAsync(request.Supervisor_Id);
        if (supervisor is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (supervisor.Type != UserType.Supervisor)
            throw new ForbiddenException(ErrorMessages.UserNotIntern);

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
