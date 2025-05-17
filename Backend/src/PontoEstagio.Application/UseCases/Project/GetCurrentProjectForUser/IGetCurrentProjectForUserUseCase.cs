using PontoEstagio.Communication.Responses;

namespace PontoEstagio.Application.UseCases.Project.GetCurrentProjectForUser;

public interface IGetCurrentProjectForUserUseCase
{
    Task<ResponseProjectJson?> Execute();
}