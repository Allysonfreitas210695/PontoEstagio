using System.Net;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;


public class InvalidLoginException : PontoEstagioException
{
    public InvalidLoginException() : base(ErrorMessages.InvalidLogin) 
    {

    }
    public override List<string> GetErrors => [Message];

    public override int GetStatusError() => (int)HttpStatusCode.Unauthorized;
}