using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Repositories;
public class UserRefreshTokenRepository : IUserRefreshTokenRepository
{
    private readonly PontoEstagioDbContext _context;
    public UserRefreshTokenRepository(PontoEstagioDbContext context)
    {
        _context = context;
    }
        
    public async Task<UserRefreshToken?> GetByToken(string token)
    {
        return await _context.UserRefreshTokens.FirstOrDefaultAsync(urt => urt.Token == token);
    }

    public async Task InsertAsync(UserRefreshToken userRefreshToken)
    {
        await _context.UserRefreshTokens.AddAsync(userRefreshToken); 
    }
}
 