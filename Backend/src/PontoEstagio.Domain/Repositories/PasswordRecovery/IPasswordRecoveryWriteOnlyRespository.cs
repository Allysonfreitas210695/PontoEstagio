namespace PontoEstagio.Domain.Repositories.PasswordRecovery;
public interface IPasswordRecoveryWriteOnlyRespository
{
    Task AddAsync(Entities.PasswordRecovery passwordRecovery);
}
