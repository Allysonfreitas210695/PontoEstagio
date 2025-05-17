using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Cource;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Cource.GetCourceById;

public class GetCourceByIdUseCase : IGetCourceByIdUseCase
{
    private readonly ICourceReadOnlyRepository _courceReadOnlyRepository;

    public GetCourceByIdUseCase(ICourceReadOnlyRepository courceReadOnlyRepository)
    {
        _courceReadOnlyRepository = courceReadOnlyRepository;
    }

    public async Task<ResponseCourceJson> Execute(Guid id)
    {
        var course = await _courceReadOnlyRepository.GetByIdAsync(id);
        if (course is null)
            throw new Exception(ErrorMessages.CourseNotFound);

        return new ResponseCourceJson() {
            Id = course.Id,
            Name = course.Name,
            WorkloadHours = course.WorkloadHours,
            University = new ResponseUniversityJson
            {
                Id = course.University.Id,
                Name = course.University.Name
            },
            CreatedAt = course.CreatedAt
        };
    }
}