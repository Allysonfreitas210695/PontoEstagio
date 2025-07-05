using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.University.Register;

public interface IRegisterUniversityUseCase
{
    Task<ResponseUniversityJson> Execute(RequestRegisterUniversityJson request);
}