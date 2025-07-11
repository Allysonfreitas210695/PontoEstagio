using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class CompanyRepository : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository, ICompanyUpdateOnlyRepository
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

    public async Task<bool> ExistsByCNPJAsync(string cnpj)
    {
        return await _dbContext.Companies.AsNoTracking().AnyAsync(c => c.CNPJ == cnpj);
    }

    public Task<bool> ExistsByEmailAsync(string email)
    {
        return _dbContext.Companies.AsNoTracking().AnyAsync(c => c.Email.Endereco == email);
    }

    public async Task<List<Company>> GetAllCompanyAsync()
    {
        return await _dbContext.Companies.AsNoTracking().ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Update(Company company)
    {
        _dbContext.Companies.Update(company);
    }

    async Task<Company?> ICompanyReadOnlyRepository.GetByIdAsync(Guid id)
    {
        return await _dbContext.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
}