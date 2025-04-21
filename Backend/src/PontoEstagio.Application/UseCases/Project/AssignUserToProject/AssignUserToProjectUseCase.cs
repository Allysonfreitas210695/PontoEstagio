using System.Data;
using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Domain.Repositories.UserProjects;
using PontoEstagio.Exceptions.Exceptions;

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
            throw new ForbiddenException();

        var intern = await _userReadOnlyRepository.GetUserByIdAsync(request.User_Id);
        if (intern is null)
            throw new NotFoundException("User does not exist.");

        if (intern.Type != UserType.Intern)
            throw new ForbiddenException();

        var alreadyAssignedToProject = await _userProjectsReadOnlyRepository
            .ExistsProjectAssignedToUserAsync(projectId, intern.Id);

        if (alreadyAssignedToProject)
            throw new NotFoundException("User is already assigned to this project.");

        var existingProject = await _userProjectsReadOnlyRepository
            .GetCurrentProjectForUserAsync(intern.Id);

        if (existingProject != null)
            throw new NotFoundException("User already has an ongoing project and cannot be assigned to another.");

        var supervisorAssigned = await _userProjectsReadOnlyRepository
            .ExistsProjectAssignedToUserAsync(projectId, supervisor.Id);

        if (supervisorAssigned)
            throw new NotFoundException("Supervisor is already assigned to this project.");

        var supervisorProject = new UserProject(supervisor.Id, projectId, supervisor.Type);
        var internProject = new UserProject(intern.Id, projectId, intern.Type);

       await _userProjectsWriteOnlyRepository.AddUserToProjectAsync(supervisorProject);
       await _userProjectsWriteOnlyRepository.AddUserToProjectAsync(internProject);

        await _unitOfWork.CommitAsync();
    }
}
