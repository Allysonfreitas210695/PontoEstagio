namespace PontoEstagio.Domain.Repositories.Comapany;

public interface ICompanyReadOnlyRepository
{
    Task<Domain.Entities.Company?> GetByIdAsync(Guid id);
    Task<List<Domain.Entities.Company>> GetAllCompanyAsync();
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByCNPJAsync(string cnpj);
}