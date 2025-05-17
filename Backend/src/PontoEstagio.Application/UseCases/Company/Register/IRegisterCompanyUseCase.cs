using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Company.Register;

public interface IRegisterCompanyUseCase
{
    Task<ResponseCompanyJson> Execute(RequestRegisterCompanytJson request);
}
