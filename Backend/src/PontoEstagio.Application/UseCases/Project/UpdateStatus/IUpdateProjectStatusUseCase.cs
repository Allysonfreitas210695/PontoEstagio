using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Application.UseCases.Projects.UpdateStatus;
public interface IUpdateProjectStatusUseCase
{
    Task Execute(Guid projectId, ProjectStatus newStatus);
}
