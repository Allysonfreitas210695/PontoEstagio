using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Projects; 
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Projects.GetProjectById;

public class GetProjectByIdUseCase : IGetProjectByIdUseCase
{
    private readonly IProjectReadOnlyRepository _projectRepository;

    public GetProjectByIdUseCase(IProjectReadOnlyRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResponseProjectJson> Execute(Guid id)
    {
        var _project = await _projectRepository.GetProjectByIdAsync(id);
        if(_project is null)
            throw new NotFoundException(ErrorMessages.ProjectNotFound);

        return new ResponseProjectJson(){
            Id = _project.Id,
            Name = _project.Name,
            Description = _project.Description, 
            StartDate = _project.StartDate,
            EndDate = _project.EndDate,
            Status = _project.Status.ToString(),
            CreatedAt = _project.CreatedAt,
            Attendances = _project.Attendances.Select(z => new ResponseAttendanceJson()
            {
                Id = z.Id,
                CheckIn = z.CheckIn,
                Status = z.Status.ToString(),
                Date = z.Date,
                CheckOut = z.CheckOut,
                UserId = z.UserId,
                CreatedAt = z.CreatedAt
            }).ToList()
        };
     }
}