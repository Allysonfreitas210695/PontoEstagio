using PontoEstagio.Communication.Responses;
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
            Activities = _project.Activities.Select(x => new ResponseActivityJson()
            {
                Id = x.Id,
                Attendance = new ResponseAttendanceJson()
                {
                    Id = x.Attendance.Id,
                    UserId = x.Attendance.UserId,
                    CheckIn = x.Attendance.CheckIn,
                    CheckOut = x.Attendance.CheckOut,
                    Date = x.Attendance.Date,
                    Status = x.Attendance.Status.ToString(),
                },
                UserId = x.UserId,
                RecordedAt = x.RecordedAt,
                ProofFilePath = x.ProofFilePath,
                Project = new ResponseProjectJson()
                {
                    Id = x.Project.Id,
                    Name = x.Project.Name,
                    Description = x.Project.Description,
                    Status = x.Project.Status.ToString(),
                    StartDate = x.Project.StartDate,
                    EndDate = x.Project.EndDate,
                },
                Description = x.Description,
            }).ToList()
        };
     }
}