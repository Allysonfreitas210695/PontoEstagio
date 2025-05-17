using Microsoft.Extensions.Configuration;
using PontoEstagio.Domain.Helpers;
using PontoEstagio.Domain.Services.Storage;

namespace PontoEstagio.Infrastructure.Services.Storage;

public class FileStorageService : IFileStorage
{
    private readonly IConfiguration _configuration;

    public FileStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> SaveBase64FileAsync(string base64)
    {
        var mimeType = Base64UtilsHelper.GetMimeTypeFromBase64(base64);
        var fileBytes = Convert.FromBase64String(base64);
        var fileName = $"{Guid.NewGuid()}.{mimeType.Split('/')[1]}";

        var relativePath = _configuration.GetValue<string>("FileStorageSettings:TempProofPath") ?? "wwwroot/uploads/temp";
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);

        var filePath = Path.Combine(fullPath, fileName);
        await File.WriteAllBytesAsync(filePath, fileBytes);

        return filePath;
    }
}