
using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Exceptions.Exceptions;

namespace PontoEstagio.Application.UseCases.Projects.GetAllProjects;
public class GetAllProjectsByUserUseCase : IGetAllProjectsUseCase
{
    private readonly IProjectOnlyRepository _projectRepository;
    private readonly ILoggedUser _loggedUser;

    public GetAllProjectsByUserUseCase(
        IProjectOnlyRepository projectRepository,
        ILoggedUser loggedUser
    )
    {
        _projectRepository = projectRepository;
       _loggedUser = loggedUser;
    }

    public async Task<List<ResponseShortProjectJson>> Execute()
    {   
        var _user = await _loggedUser.Get();

        if(_user is null) 
            throw new NotFoundException("user not exists.");

        var listaProjects = new List<ResponseShortProjectJson>();

        if (_user.Type == UserType.Supervisor)
        {
           var _projects = await _projectRepository.GetAllProjectsBySupervisorAsync(_user);
           listaProjects.AddRange(_projects.Select(z => new ResponseShortProjectJson
           {
               Id = z.Id,
               Name = z.Name,
               Description = z.Description,
               Status = z.Status.ToString(),
               StartDate = z.StartDate,
               EndDate = z.EndDate
           }).ToList());
        }

        if (_user.Type == UserType.Intern)
        {
            var _projects = await _projectRepository.GetAllProjectsByInternAsync(_user);
            listaProjects.AddRange(_projects.Select(z => new ResponseShortProjectJson
            {
                Id = z.Id,
                Name = z.Name,
                Description = z.Description,
                Status = z.Status.ToString(),
                StartDate = z.StartDate,
                EndDate = z.EndDate
            }).ToList());
        }

        return listaProjects;
    }
}
