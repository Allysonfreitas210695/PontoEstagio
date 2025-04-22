namespace PontoEstagio.Domain.Repositories.Comapany;

public interface ICompanyReadOnlyRepository
{
    Task<List<Domain.Entities.Company>> GetAllCompanyAsync();
}