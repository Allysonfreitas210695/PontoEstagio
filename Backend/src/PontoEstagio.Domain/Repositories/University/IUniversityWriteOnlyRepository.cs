namespace PontoEstagio.Domain.Repositories.University;

public interface IUniversityWriteOnlyRepository
{
    Task AddAsync(Entities.University university);
}