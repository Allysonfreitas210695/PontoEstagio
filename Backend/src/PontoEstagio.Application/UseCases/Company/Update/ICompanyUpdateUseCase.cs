using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Company.Update;

public interface ICompanyUpdateUseCase
{
    Task Execute(Guid id, RequestRegisterCompanytJson request);
}