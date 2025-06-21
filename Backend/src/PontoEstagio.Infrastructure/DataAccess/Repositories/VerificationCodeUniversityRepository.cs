using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.VerificationCodeUniversity;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;

public class VerificationCodeUniversityRepository :
    IVerificationCodeUniversityOnlyReadRepository,
    IVerificationCodeUniversityOnlyUpdateRepository,
    IVerificationCodeUniversityOnlyWriteRepository
{
    private readonly PontoEstagioDbContext _dbContext;

    public VerificationCodeUniversityRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(VerificationCodeUniversity verificationCode)
    {
        await _dbContext.VerificationCodeUniversities.AddAsync(verificationCode);
    }

    public async Task<VerificationCodeUniversity?> GetByEmailAsync(string email)
    {
        return await _dbContext.VerificationCodeUniversities
                                .AsNoTracking()
                                .FirstOrDefaultAsync(v => v.Email.Endereco == email);
    }

    async Task<VerificationCodeUniversity?> IVerificationCodeUniversityOnlyReadRepository.GetByCodeAsync(string code)
    {
        return await _dbContext.VerificationCodeUniversities
                                .AsNoTracking()
                                .FirstOrDefaultAsync(v => v.Code == code);
    }

    async Task<VerificationCodeUniversity?> IVerificationCodeUniversityOnlyUpdateRepository.GetByCodeAsync(string code)
    {
        return await _dbContext.VerificationCodeUniversities
                                    .FirstOrDefaultAsync(v => v.Code == code);
    }

    public async Task<bool> ExistsActiveCodeForEmailAsync(string email)
    {
        return await _dbContext.VerificationCodeUniversities
            .AsNoTracking()
            .AnyAsync(v =>
                v.Email.Endereco == email &&
                v.Expiration > DateTime.UtcNow &&
                v.Status == Domain.Enum.VerificationCodeStatus.Active);
    }

    public Task UpdateAsync(VerificationCodeUniversity verificationCode)
    {
        throw new NotImplementedException();
    }
}
