namespace PontoEstagio.Exceptions.Exceptions;

public abstract class PontoEstagioException : SystemException
{
    protected PontoEstagioException(string message) : base(message)
    {

    }

    public abstract List<string> GetErrors { get; }
    public abstract int GetStatusError();
}