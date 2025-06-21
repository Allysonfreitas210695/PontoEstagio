namespace PontoEstagio.Domain.Repositories.LegalRepresentative;

public interface ILegalRepresentativeUpdateOnlyRepository
{
    Task<Domain.Entities.LegalRepresentative?> GetByIdAsync(Guid companyId, Guid legalRepresentativeId);
    void Update(Domain.Entities.LegalRepresentative legalRepresentative);
}