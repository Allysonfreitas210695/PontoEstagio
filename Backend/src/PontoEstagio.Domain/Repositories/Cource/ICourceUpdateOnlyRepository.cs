namespace PontoEstagio.Domain.Repositories.Cource;

public interface ICourceUpdateOnlyRepository
{
    Task<Entities.Course?> GetByIdAsync(Guid id);
    void Update(Entities.Course course);
}