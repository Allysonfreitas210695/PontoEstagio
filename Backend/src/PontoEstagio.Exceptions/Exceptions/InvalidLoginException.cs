using System.Net;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Exceptions.Exceptions;

public class InvalidLoginException : PontoEstagioException
{
    public InvalidLoginException() : base(ErrorMessages.InvalidLogin) 
    {

    }
    public override List<string> GetErrors => [Message];

    public override int GetStatusError() => (int)HttpStatusCode.Unauthorized;
}