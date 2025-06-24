using System.Net; 

namespace PontoEstagio.Exceptions.Exceptions;
public class ForbiddenException : PontoEstagioException
{
    public ForbiddenException(string Message) : base(Message)
    {
    }

    public override List<string> GetErrors => [Message];

    public override int GetStatusError() => (int)HttpStatusCode.Forbidden;
}

