
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories.UserProjects;
using PontoEstagio.Exceptions.ResourcesErrors;


namespace PontoEstagio.Application.UseCases.Projects.DeleteUserFromProject;
public class DeleteUserFromProjectUseCase : IDeleteUserFromProjectUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserProjectsUpdateOnlyRepository _userProjectsUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserFromProjectUseCase(
        ILoggedUser loggedUser,
        IUserProjectsUpdateOnlyRepository userProjectsUpdateOnlyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _loggedUser = loggedUser;
        _userProjectsUpdateOnlyRepository = userProjectsUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid projectId, Guid userId)
    {
        var supervisor = await _loggedUser.Get();

        if (supervisor is null || supervisor.Type != UserType.Supervisor)
            throw new ForbiddenException(ErrorMessages.SupervisorNotFoundOrNotSupervisor);

        if (supervisor.Id == userId)
            throw new BusinessRuleException(ErrorMessages.CannotRemoveSelfFromProject);

        var userProject = await _userProjectsUpdateOnlyRepository.GetActiveUserProjectAsync(projectId, userId);
        if (userProject is null)
            throw new NotFoundException(ErrorMessages.AssociationNotFoundOrInactive);

        userProject.MarkAsInactive();

        var supervisorProject = await _userProjectsUpdateOnlyRepository.GetActiveUserProjectAsync(projectId, supervisor.Id);
        if (supervisorProject is null)
            throw new NotFoundException(ErrorMessages.SupervisorAssociationNotFound);

        supervisorProject.MarkAsInactive();

        _userProjectsUpdateOnlyRepository.Update(userProject);
        _userProjectsUpdateOnlyRepository.Update(supervisorProject);

        await _unitOfWork.CommitAsync();
    }
}
