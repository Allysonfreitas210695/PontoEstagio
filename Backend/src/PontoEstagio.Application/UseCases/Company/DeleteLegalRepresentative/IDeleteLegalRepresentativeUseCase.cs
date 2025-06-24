namespace PontoEstagio.Application.UseCases.Company.DeleteLegalRepresentative;

public interface IDeleteLegalRepresentativeUseCase
{
    Task Execute(Guid companyId, Guid representativeId);
}