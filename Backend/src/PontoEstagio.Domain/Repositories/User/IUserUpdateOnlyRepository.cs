namespace PontoEstagio.Domain.Repositories.User;

public interface IUserUpdateOnlyRepository
{
    Task<Entities.User?> GetUserByIdAsync(Guid id);
    Task<Entities.User?> GetUserByEmailAsync(string email);
    void Update(Entities.User user);

    Task<bool> ExistOtherUserWithSameRegistrationAsync(Guid userId, string registration);
}
