
namespace PontoEstagio.Communication.Requests;

public class RequestRegisterActivityJson
{
    public Guid AttendanceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ProofFilePath { get; set; } = string.Empty;
}
