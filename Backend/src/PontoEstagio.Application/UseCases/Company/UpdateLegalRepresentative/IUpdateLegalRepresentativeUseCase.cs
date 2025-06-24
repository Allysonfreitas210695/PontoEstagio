using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Company.UpdateLegalRepresentative;

public interface IUpdateLegalRepresentativeUseCase
{
        Task Execute(Guid representativeId, RequestRegisterLegalRepresentativeJson request);
}