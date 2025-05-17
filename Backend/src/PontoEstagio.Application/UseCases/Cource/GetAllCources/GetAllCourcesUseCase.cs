using PontoEstagio.Communication.Responses;
using PontoEstagio.Domain.Repositories.Cource;

namespace PontoEstagio.Application.UseCases.Cource.GetAllCources;

public class GetAllCourcesUseCase : IGetAllCourcesUseCase
{
    private readonly ICourceReadOnlyRepository _courceReadOnlyRepository;

    public GetAllCourcesUseCase(ICourceReadOnlyRepository courceReadOnlyRepository)
    {
        _courceReadOnlyRepository = courceReadOnlyRepository;
    }

    public async Task<List<ResponseCourceJson>> Execute()
    {
        var courses = await _courceReadOnlyRepository.GetAllAsync();
        return courses.Select(c => new ResponseCourceJson
        {
            Id = c.Id,
            Name = c.Name,
            WorkloadHours = c.WorkloadHours,
            University = new ResponseUniversityJson
            {
                Id = c.University.Id,
                Name = c.University.Name
            },
            CreatedAt = c.CreatedAt
        }).ToList();
    }
}