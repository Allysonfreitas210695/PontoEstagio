namespace PontoEstagio.Communication.Responses;
public class ResponseReportMonthlyJson
{
    public string Reference { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public double TotalHours { get; set; }
}
