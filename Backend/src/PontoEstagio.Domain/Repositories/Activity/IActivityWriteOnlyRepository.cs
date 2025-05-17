namespace PontoEstagio.Domain.Repositories.Activity;
public interface IActivityWriteOnlyRepository
{
    Task AddAsync(Entities.Activity activity);
}
