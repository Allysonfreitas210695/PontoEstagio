using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Company.GetAllCompany;

public interface IGetAllCompanyUseCase
{
    Task<List<ResponseCompanyJson>> Execute();
}