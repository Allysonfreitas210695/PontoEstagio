 namespace PontoEstagio.Domain.Repositories.PasswordRecovery;
public interface IPasswordRecoveryUpdateOnlyRespository
{
    Task<Entities.PasswordRecovery?> GetPasswordRecoveryByCode(string code, Guid userId);

    void Update(Entities.PasswordRecovery passwordRecovery);
}
