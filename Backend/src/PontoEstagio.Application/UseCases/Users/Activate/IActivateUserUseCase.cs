namespace PontoEstagio.Application.UseCases.Users.Delete;
public interface IActivateUserUseCase
{
    Task Execute(Guid id);
}
