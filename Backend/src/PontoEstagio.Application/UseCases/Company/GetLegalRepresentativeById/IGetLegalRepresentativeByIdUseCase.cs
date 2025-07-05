using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Company.GetLegalRepresentativeById;
public interface IGetLegalRepresentativeByIdUseCase
{
    Task<ResponseLegalRepresentativeJson> Execute(Guid companyId, Guid representativeId);
}

