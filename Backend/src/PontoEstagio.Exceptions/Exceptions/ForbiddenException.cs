using System.Net; 

namespace PontoEstagio.Exceptions.Exceptions;
public class ForbiddenException : PontoEstagioException
{
    public ForbiddenException() : base("You do not have permission to perform this action.")
    {
    }

    public override List<string> GetErrors => [Message];

    public override int GetStatusError() => (int)HttpStatusCode.Forbidden;
}

