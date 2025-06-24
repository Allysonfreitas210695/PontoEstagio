namespace PontoEstagio.Domain.Repositories.PasswordRecovery;
public interface IPasswordRecoveryReadOnlyRespository
{
    Task<Entities.PasswordRecovery?> GetPasswordRecoveryByCode(string code);
}
