using PontoEstagio.Communication.Enum;

namespace PontoEstagio.Communication.Request;
public class RequestUpdateProjectStatusJson
{
    public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
}
