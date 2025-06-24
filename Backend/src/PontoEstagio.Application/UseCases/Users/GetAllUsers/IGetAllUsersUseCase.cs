using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Users.GetAllUsers;
public interface IGetAllUsersUseCase
{
    Task<List<ResponseShortUserJson>> Execute();
}
