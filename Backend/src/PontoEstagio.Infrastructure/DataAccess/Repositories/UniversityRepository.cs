using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.University;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class UniversityRepository : IUniversityReadOnlyRepository, IUniversityWriteOnlyRepository, IUniversityUpdateOnlyRepository
{
    private readonly PontoEstagioDbContext _dbContext;
    public UniversityRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(University university)
    {
        await _dbContext.Universities.AddAsync(university);
    }

    public async Task<bool> ExistsByCNPJAsync(string cnpj)
    {
        return await _dbContext.Universities.AsNoTracking().AnyAsync(c => c.CNPJ == cnpj);
    }

    public async Task<bool> ExistsByCNPJAsync(Guid id, string cnpj)
    {
        return await _dbContext.Universities.AsNoTracking().AnyAsync(c => c.Id != id && c.CNPJ == cnpj);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.Universities.AsNoTracking().AnyAsync(c => c.Email.Endereco == email);
    }

    public  async Task<bool> ExistsByEmailAsync(Guid id, string email)
    {
        return await _dbContext.Universities.AsNoTracking().AnyAsync(c => c.Id != id && c.Email.Endereco == email);
    }

    public async Task<List<University>> GetAllUniversitiesAsync()
    {
        return await _dbContext.Universities.AsNoTracking().ToListAsync();
    }

    async Task<University?> IUniversityUpdateOnlyRepository.GetUniversityByIdAsync(Guid id)
    {
        return await _dbContext.Universities.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public void Update(University university)
    {
        _dbContext.Universities.Update(university);
    }

    async Task<University?> IUniversityReadOnlyRepository.GetUniversityByIdAsync(Guid id)
    {
        return await _dbContext.Universities.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }
}