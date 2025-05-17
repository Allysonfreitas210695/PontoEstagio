using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Users.GetUserById;
public interface IGetUserByIdUseCase
{
    Task<ResponseUserJson> Execute(Guid id);
}
