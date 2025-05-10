using PontoEstagio.Communication.Requests;
using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Activity.Create
{
    public interface ICreateActivityUseCase
    {
        Task<ResponseActivityJson> ExecuteAsync(CreateActivityRequest request);
    }
}
