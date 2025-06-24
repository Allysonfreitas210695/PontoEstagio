using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.LegalRepresentative;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class LegalRepresentativeRepository : ILegalRepresentativeReadOnlyRepository, ILegalRepresentativeWriteOnlyRepository, ILegalRepresentativeUpdateOnlyRepository, ILegalRepresentativeDeleteOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;

    public LegalRepresentativeRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(LegalRepresentative legalRepresentative)
    {
        await _dbContext.LegalRepresentatives.AddAsync(legalRepresentative);
    }

    public async Task<bool> ExistsByCPFAsync(string cpf)
    {
        return await _dbContext.LegalRepresentatives.AsNoTracking().AnyAsync(lr => lr.CPF == cpf);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.LegalRepresentatives.AsNoTracking().AnyAsync(c => c.Email.Endereco == email);
    }

    public async Task<List<LegalRepresentative>> GetAllLegalRepresentativeAsync(Guid companyId)
    {
        return await _dbContext.LegalRepresentatives.Where(x => x.CompanyId == companyId).Include(x => x.Company).AsNoTracking().ToListAsync();
    }

    async  Task<LegalRepresentative?> ILegalRepresentativeReadOnlyRepository.GetByIdAsync(Guid companyId, Guid legalRepresentativeId)
    {
        return await _dbContext.LegalRepresentatives.AsNoTracking().FirstOrDefaultAsync(lr => lr.CompanyId == companyId && lr.Id == legalRepresentativeId);
    }

    async Task<LegalRepresentative?> ILegalRepresentativeUpdateOnlyRepository.GetByIdAsync(Guid companyId, Guid legalRepresentativeId)
    {
        return await _dbContext.LegalRepresentatives.FirstOrDefaultAsync(lr => lr.CompanyId == companyId && lr.Id == legalRepresentativeId);
    }

    public void Update(LegalRepresentative legalRepresentative)
    {
        _dbContext.LegalRepresentatives.Update(legalRepresentative);
    }

    async Task<LegalRepresentative?> ILegalRepresentativeDeleteOnlyRepository.GetByIdAsync(Guid companyId, Guid legalRepresentativeId)
    {
        return await _dbContext.LegalRepresentatives.FirstOrDefaultAsync(lr => lr.CompanyId == companyId && lr.Id == legalRepresentativeId);
    }

    public void Delete(LegalRepresentative legalRepresentative)
    {
        _dbContext.LegalRepresentatives.Remove(legalRepresentative);
    }
}