using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class CompanyRepository : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository
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

    public async Task<List<Company>> GetAllCompanyAsync()
    {
        return await _dbContext.Companies.AsNoTracking().ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
}