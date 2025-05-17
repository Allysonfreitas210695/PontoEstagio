using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.University.GetAllUniversities;

public interface IGetAllUniversitiesUseCase
{
    Task<List<ResponseUniversityJson>> Execute();
}