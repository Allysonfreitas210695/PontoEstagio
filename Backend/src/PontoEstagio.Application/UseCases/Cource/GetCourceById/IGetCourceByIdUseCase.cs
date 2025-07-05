using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Cource.GetCourceById;

public interface IGetCourceByIdUseCase
{
    Task<ResponseCourceJson> Execute(Guid id);
}