using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Company.GetAllRepresentativesFromCompany;

public interface IGetAllRepresentativesFromCompanyUseCase
{
    Task<List<ResponseLegalRepresentativeJson>> Execute(Guid companyId);
}