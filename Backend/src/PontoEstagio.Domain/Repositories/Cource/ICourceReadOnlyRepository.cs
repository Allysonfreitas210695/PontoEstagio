namespace PontoEstagio.Domain.Repositories.Cource;

public interface ICourceReadOnlyRepository
{
    Task<Entities.Course?> GetByIdAsync(Guid id);
    Task<List<Entities.Course>> GetAllAsync();
}