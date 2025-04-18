using System.Net;

namespace PontoEstagio.Exceptions.Exceptions;

public class ErrorOnValidationException : PontoEstagioException
{
    public List<string> Errors { get; set; } = [];

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        Errors = errorMessages;
    }

    public override List<string> GetErrors => Errors;
    public override int GetStatusError() => (int)HttpStatusCode.BadRequest;
}