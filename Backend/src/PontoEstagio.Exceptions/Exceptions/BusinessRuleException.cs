using System.Net; 

namespace PontoEstagio.Exceptions.Exceptions;

public class BusinessRuleException : PontoEstagioException
{
    public BusinessRuleException(string Message) : base(Message)
    {
    }

    public override List<string> GetErrors => [Message];

    public override int GetStatusError() => (int)HttpStatusCode.Conflict;
}
