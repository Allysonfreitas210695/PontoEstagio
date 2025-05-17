namespace PontoEstagio.Domain.Services.Storage;

public interface IFileStorage
{
    Task<string> SaveBase64FileAsync(string base64);
}