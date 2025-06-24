using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Domain.Repositories.User;

public interface IUserRefreshTokenRepository
{
    Task<UserRefreshToken?> GetByToken(string token);
    Task InsertAsync(UserRefreshToken userRefreshToken);
}