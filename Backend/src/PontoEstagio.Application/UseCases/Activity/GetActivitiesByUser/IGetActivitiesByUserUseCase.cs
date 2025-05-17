using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Activity.GetActivitiesByUser;

public interface IGetActivitiesByUserUseCase
{
    Task<List<ResponseActivityJson>> Execute(Guid userId);
}