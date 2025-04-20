using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Exceptions.Exceptions;

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
             throw new NotFoundException("Project not found");

        return new ResponseProjectJson(){
            Id = _project.Id,
            Name = _project.Name,
            Description = _project.Description, 
            StartDate = _project.StartDate,
            EndDate = _project.EndDate,
            Status = _project.Status.ToString(),
            Activities = _project.Activities.Select(x => new ResponseActivityJson()
            {
                Id = x.Id,
                AttendanceId = x.AttendanceId,
                UserId = x.UserId,
                RecordedAt = x.RecordedAt,
                ProofFilePath = x.ProofFilePath,
                ProjectId = x.Id,
                Description = x.Description,
            }).ToList()
        };
     }
}