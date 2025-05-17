namespace PontoEstagio.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}