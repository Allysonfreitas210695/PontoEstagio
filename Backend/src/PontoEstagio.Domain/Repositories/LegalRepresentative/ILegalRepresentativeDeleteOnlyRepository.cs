namespace PontoEstagio.Domain.Repositories.LegalRepresentative;

public interface ILegalRepresentativeDeleteOnlyRepository
{
    Task<Domain.Entities.LegalRepresentative?> GetByIdAsync(Guid companyId, Guid legalRepresentativeId);
    void Delete(Domain.Entities.LegalRepresentative legalRepresentative);
}