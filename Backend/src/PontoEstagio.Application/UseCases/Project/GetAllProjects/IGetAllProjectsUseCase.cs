using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Projects.GetAllProjects;
public interface IGetAllProjectsUseCase
{
    Task<List<ResponseShortProjectJson>> Execute();
}
