using PontoEstagio.Communication.Requests;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Activity.Create;

public interface IRegisterActivityUseCase
{
    Task<ResponseShortActivityJson> ExecuteAsync(RequestRegisterActivityJson request);
}
