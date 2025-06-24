namespace PontoEstagio.Application.UseCases.Users.Deactivated;
public interface IDeactivatedUserUseCase
{
    Task Execute(Guid id);
}
