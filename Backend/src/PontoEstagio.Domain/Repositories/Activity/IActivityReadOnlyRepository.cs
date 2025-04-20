namespace PontoEstagio.Domain.Repositories.Activity;

public interface IActivityReadOnlyRepository
{
    Task<Entities.Activity?> GetByIdAsync(Guid id);
}