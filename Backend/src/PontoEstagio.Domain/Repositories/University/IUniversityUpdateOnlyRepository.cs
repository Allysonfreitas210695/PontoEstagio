namespace PontoEstagio.Domain.Repositories.University;

public interface IUniversityUpdateOnlyRepository
{
    Task<Entities.University?> GetUniversityByIdAsync(Guid id);
    Task<bool> ExistsByEmailAsync(Guid id, string email);
    Task<bool> ExistsByCNPJAsync(Guid id, string cnpj);
    void Update(Entities.University university);
}