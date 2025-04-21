using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Projects.GetProjectById;

public interface IGetProjectByIdUseCase
{
    Task<ResponseProjectJson> Execute(Guid id);
}