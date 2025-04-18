using PontoEstagio.Domain.Entities;

public interface ILoggedUser
{
    Task<User> Get();
}