using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.UserProjects;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Project.GetCurrentProjectForUser;

public class GetCurrentProjectForUserUseCase : IGetCurrentProjectForUserUseCase
{
    private readonly IUserProjectsReadOnlyRepository _userProjectsReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    public GetCurrentProjectForUserUseCase(
        IUserProjectsReadOnlyRepository userProjectsReadOnlyRepository,
        ILoggedUser loggedUser
    )
    {
        _userProjectsReadOnlyRepository = userProjectsReadOnlyRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseProjectJson?> Execute()
    {
        var user = await _loggedUser.Get();
        if (user == null)
            throw new Exception(ErrorMessages.UserNotFound);

        if(user.Type != Domain.Enum.UserType.Intern)
            throw new Exception(ErrorMessages.UserNotIntern);

        var _currentProject = await _userProjectsReadOnlyRepository.GetCurrentProjectForUserAsync(user.Id);
        if (_currentProject == null)
            throw new Exception(ErrorMessages.ProjectNotFound);

        return new ResponseProjectJson
        {
            Id = _currentProject.Id,
            Name = _currentProject.Name,
            Description = _currentProject.Description,
            StartDate = _currentProject.StartDate,
            EndDate = _currentProject.EndDate,
            Status = _currentProject.Status.ToString()
        };
    }
}