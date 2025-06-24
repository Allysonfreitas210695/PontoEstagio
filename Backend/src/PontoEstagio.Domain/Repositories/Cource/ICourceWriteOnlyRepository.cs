namespace PontoEstagio.Domain.Repositories.Cource;

public interface ICourceWriteOnlyRepository
{
    Task AddAsync(Entities.Course course);
}