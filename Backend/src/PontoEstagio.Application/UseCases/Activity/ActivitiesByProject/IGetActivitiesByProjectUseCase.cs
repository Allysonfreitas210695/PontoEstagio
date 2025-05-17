using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Activity.ActivitiesByProject;

public interface IGetActivitiesByProjectUseCase
{
    Task<List<ResponseActivityJson>> Execute(Guid projectId);
}