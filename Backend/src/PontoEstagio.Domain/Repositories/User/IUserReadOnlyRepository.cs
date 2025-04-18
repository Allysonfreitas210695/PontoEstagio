namespace PontoEstagio.Domain.Repositories;

public interface IUserReadOnlyRepository
{
    Task<Entities.User?> GetUserByIdAsync(Guid id);
    Task<List<Entities.User>> GetAllUsersAsync();
    Task<bool> ExistActiveUserWithEmailAsync(string email);
    Task<Entities.User?> GetUserByEmailAsync(string email);
}