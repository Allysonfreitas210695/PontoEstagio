namespace PontoEstagio.Domain.Repositories;

public interface IUserWriteOnlyRepository
{
    Task AddAsync(Entities.User user);
}