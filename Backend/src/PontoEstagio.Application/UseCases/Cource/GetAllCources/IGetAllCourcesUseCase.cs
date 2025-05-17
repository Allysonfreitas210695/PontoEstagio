using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Cource.GetAllCources;

public interface IGetAllCourcesUseCase
{
    Task<List<ResponseCourceJson>> Execute();
}