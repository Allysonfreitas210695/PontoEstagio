using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.University.GetUniversityById;

public interface IGetUniversityByIdUseCase
{
    Task<ResponseUniversityJson> Execute(Guid id);
}