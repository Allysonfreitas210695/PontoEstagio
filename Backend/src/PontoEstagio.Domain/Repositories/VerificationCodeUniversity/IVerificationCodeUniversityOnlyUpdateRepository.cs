namespace PontoEstagio.Domain.Repositories.VerificationCodeUniversity;
public interface IVerificationCodeUniversityOnlyUpdateRepository
{
    Task<Domain.Entities.VerificationCodeUniversity?> GetByCodeAsync(string code);
    Task UpdateAsync(Domain.Entities.VerificationCodeUniversity verificationCode);
}
