 
namespace PontoEstagio.Domain.Repositories.Comapany;

public interface ICompanyUpdateOnlyRepository
{
    Task<Domain.Entities.Company?> GetByIdAsync(Guid id);
    void Update(Domain.Entities.Company company);
}