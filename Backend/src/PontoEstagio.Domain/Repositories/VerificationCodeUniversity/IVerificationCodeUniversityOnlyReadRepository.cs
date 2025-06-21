namespace PontoEstagio.Domain.Repositories.VerificationCodeUniversity;
public interface IVerificationCodeUniversityOnlyReadRepository
{
    Task<Domain.Entities.VerificationCodeUniversity?> GetByEmailAsync(string email);
    Task<Domain.Entities.VerificationCodeUniversity?> GetByCodeAsync(string code);
    Task<bool> ExistsActiveCodeForEmailAsync(string email);
}
