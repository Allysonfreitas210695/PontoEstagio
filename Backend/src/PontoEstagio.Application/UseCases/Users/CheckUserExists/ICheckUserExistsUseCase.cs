using PontoEstagio.Communication.Request;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Users.CheckUserExists;

public interface ICheckUserExistsUseCase
{
    Task<ResponseCheckUserUserJson> Execute(RequestCheckUserExistsUserJson request);
}