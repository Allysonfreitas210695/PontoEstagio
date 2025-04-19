using System.Data;
using PontoEstagio.Communication.Request;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Projects.AssignUserToProject;

public class AssignUserToProjectUseCase : IAssignUserToProjectUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectReadOnlyRepository _projectReadOnlyRepository;
    private readonly IProjectWriteOnlyRepository _projectWriteOnlyRepository;

    public AssignUserToProjectUseCase(
        ILoggedUser loggedUser,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IProjectReadOnlyRepository projectReadOnlyRepository,
        IProjectWriteOnlyRepository projectWriteOnlyRepository
    )
    {
        _loggedUser = loggedUser;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _projectReadOnlyRepository = projectReadOnlyRepository;
        _projectWriteOnlyRepository = projectWriteOnlyRepository;
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

        var alreadyAssignedToProject = await _projectReadOnlyRepository
            .ExistsProjectAssignedToUserAsync(projectId, intern.Id);

        if (alreadyAssignedToProject)
            throw new NotFoundException("User is already assigned to this project.");

        var existingProject = await _projectReadOnlyRepository
            .GetCurrentProjectForUserAsync(intern.Id);

        if (existingProject != null)
            throw new NotFoundException("User already has an ongoing project and cannot be assigned to another.");

        var supervisorAssigned = await _projectReadOnlyRepository
            .ExistsProjectAssignedToUserAsync(projectId, supervisor.Id);

        if (supervisorAssigned)
            throw new NotFoundException("Supervisor is already assigned to this project.");

        var supervisorProject = new UserProject(supervisor.Id, projectId, supervisor.Type);
        var internProject = new UserProject(intern.Id, projectId, intern.Type);

       await _projectWriteOnlyRepository.AddUserToProjectAsync(supervisorProject);
       await _projectWriteOnlyRepository.AddUserToProjectAsync(internProject);

        await _unitOfWork.CommitAsync();
    }
}
