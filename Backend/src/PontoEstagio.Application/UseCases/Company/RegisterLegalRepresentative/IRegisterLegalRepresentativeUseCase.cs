using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Company.RegisterLegalRepresentative;

public interface IRegisterLegalRepresentativeUseCase
{
    Task Execute(RequestRegisterLegalRepresentativeJson request);
}