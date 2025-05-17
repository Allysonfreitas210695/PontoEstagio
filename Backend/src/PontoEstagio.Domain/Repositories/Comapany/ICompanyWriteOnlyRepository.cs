namespace PontoEstagio.Domain.Repositories.Comapany;

public interface ICompanyWriteOnlyRepository
{
    Task AddAsync(Entities.Company company);
}