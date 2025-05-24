using Microsoft.Extensions.Configuration;
using PontoEstagio.Domain.Services.Storage;

namespace PontoEstagio.Infrastructure.Services.Storage;

public class FileStorageService : IFileStorage
{
    private readonly IConfiguration _configuration;

    public FileStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string GetMimeTypeFromBase64(string base64)
    {
        var data = Convert.FromBase64String(base64);

        if (data.Length < 4) return "application/octet-stream";
        
        if (data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46)
            return "application/pdf";
            
        if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
            return "image/png";
            
        if (data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
            return "image/jpeg";
            
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46)
            return "image/gif";

        if (data[0] == 0x50 && data[1] == 0x4B && data[2] == 0x03 && data[3] == 0x04)
            return "application/zip";  

        if (data[0] == 0x49 && data[1] == 0x44 && data[2] == 0x33)
            return "audio/mpeg"; 

        if (data[4] == 0x66 && data[5] == 0x74 && data[6] == 0x79 && data[7] == 0x70)
            return "video/mp4"; 

        return "application/octet-stream";
    }


    public static string GetExtensionFromMimeType(string mimeType)
    {
        return mimeType switch
        {
            "application/pdf" => "pdf",
            "image/png" => "png",
            "image/jpeg" => "jpg",
            "image/gif" => "gif",
            "image/bmp" => "bmp",
            "application/msword" => "doc",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => "docx",
            "application/vnd.ms-excel" => "xls",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "xlsx",
            "application/vnd.ms-powerpoint" => "ppt",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation" => "pptx",
            "application/zip" => "zip",
            "application/x-rar-compressed" => "rar",
            "application/x-7z-compressed" => "7z",
            "text/plain" => "txt",
            "text/html" => "html",
            "text/csv" => "csv",
            "application/json" => "json",
            "audio/mpeg" => "mp3",
            "audio/wav" => "wav",
            "video/mp4" => "mp4",
            "video/x-msvideo" => "avi",
            "application/octet-stream" => "bin", 
            _ => "bin"
        };
    }

    public async Task<string> SaveBase64FileAsync(string base64)
    {
        var mimeType = GetMimeTypeFromBase64(base64);
        var extension = GetExtensionFromMimeType(mimeType);

        var fileBytes = Convert.FromBase64String(base64);
        var fileName = $"{Guid.NewGuid()}.{extension}";

        var relativePath = _configuration.GetValue<string>("FileStorageSettings:TempProofPath") ?? "wwwroot/uploads/temp";
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);

        var filePath = Path.Combine(fullPath, fileName);
        await File.WriteAllBytesAsync(filePath, fileBytes);

        return fileName;
    }
}