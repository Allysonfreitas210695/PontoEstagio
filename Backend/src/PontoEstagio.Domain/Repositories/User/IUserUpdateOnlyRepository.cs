namespace PontoEstagio.Domain.Repositories.User;

public interface IUserUpdateOnlyRepository
{
    Task<Entities.User?> GetUserByIdAsync(Guid id);
    void Update(Entities.User user);
}
