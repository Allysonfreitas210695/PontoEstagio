using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class CompanyRepository : ICompanyWriteOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;

    public CompanyRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Company company)
    {
        await _dbContext.Companies.AddAsync(company);
    }
}