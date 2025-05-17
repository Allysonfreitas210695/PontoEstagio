namespace PontoEstagio.Domain.Repositories.University;

public interface IUniversityReadOnlyRepository
{
    Task<Entities.University?> GetUniversityByIdAsync(Guid id);
    Task<List<Entities.University>> GetAllUniversitiesAsync();
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByCNPJAsync(string cnpj);
}