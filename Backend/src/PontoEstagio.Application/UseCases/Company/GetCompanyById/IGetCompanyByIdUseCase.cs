

using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Company.GetCompanyById;

public interface IGetCompanyByIdUseCase
{
    Task<ResponseCompanyJson> Execute(Guid id);
}