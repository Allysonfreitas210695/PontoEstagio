namespace PontoEstagio.Domain.Repositories.LegalRepresentative;

public interface ILegalRepresentativeReadOnlyRepository
{
    Task<Domain.Entities.LegalRepresentative?> GetByIdAsync(Guid companyId, Guid legaRepresentativeId);
    Task<List<Domain.Entities.LegalRepresentative>> GetAllLegalRepresentativeAsync(Guid companyId);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByCPFAsync(string cpf);
}