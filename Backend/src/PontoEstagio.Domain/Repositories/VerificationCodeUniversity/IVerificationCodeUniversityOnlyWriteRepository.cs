namespace PontoEstagio.Domain.Repositories.VerificationCodeUniversity;
public interface IVerificationCodeUniversityOnlyWriteRepository
{
    Task AddAsync(Domain.Entities.VerificationCodeUniversity verificationCode);
}
