using System.Net;
 
namespace PontoEstagio.Exceptions.Exceptions;

public class InvalidLoginException : PontoEstagioException
{
    public InvalidLoginException() : base("Invalid login. Please check your email and password.") 
    {

    }
    public override List<string> GetErrors => [Message];

    public override int GetStatusError() => (int)HttpStatusCode.Unauthorized;
}