namespace PontoEstagio.Domain.Repositories.LegalRepresentative;

public interface ILegalRepresentativeWriteOnlyRepository
{
    Task AddAsync(Entities.LegalRepresentative legalRepresentative);
}