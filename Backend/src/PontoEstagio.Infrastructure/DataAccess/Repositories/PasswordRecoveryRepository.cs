
using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.PasswordRecovery;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;
public class PasswordRecoveryRepository : IPasswordRecoveryReadOnlyRespository, IPasswordRecoveryUpdateOnlyRespository, IPasswordRecoveryWriteOnlyRespository
{
    private readonly PontoEstagioDbContext _dbContext;
    public PasswordRecoveryRepository(PontoEstagioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    async Task<PasswordRecovery?> IPasswordRecoveryReadOnlyRespository.GetPasswordRecoveryByCode(string code)
    {
        return await _dbContext.PasswordRecoveries
                                .AsNoTracking()
                                .Where(x => x.Code == code && !x.Used)
                                .OrderByDescending(x => x.CreatedAt)
                                .FirstOrDefaultAsync();
    
    }
    
    async Task<PasswordRecovery?> IPasswordRecoveryUpdateOnlyRespository.GetPasswordRecoveryByCode(string code)
    {
        return await _dbContext.PasswordRecoveries 
                                .Where(x => x.Code == code && !x.Used)
                                .OrderByDescending(x => x.CreatedAt)
                                .FirstOrDefaultAsync();
    }

    public void Update(PasswordRecovery passwordRecovery)
    {
        _dbContext.PasswordRecoveries.Update(passwordRecovery);
    }

    public async Task AddAsync(PasswordRecovery passwordRecovery)
    {
        await _dbContext.PasswordRecoveries.AddAsync(passwordRecovery);
    }
}
