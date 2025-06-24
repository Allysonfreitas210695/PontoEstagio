using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Activity.GetActivityById;

public interface IGetActivityByIdUseCase
{
    Task<ResponseActivityJson> Execute(Guid id);
}