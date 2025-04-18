namespace PontoEstagio.Communication.Responses;

public class ResponseLoggedUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}