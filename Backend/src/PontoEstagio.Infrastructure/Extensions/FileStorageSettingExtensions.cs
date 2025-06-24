using Microsoft.Extensions.Configuration;
using System.IO;

namespace PontoEstagio.Infrastructure.Extensions
{
    public static class FileStorageSettingExtensions
    {
        public static string FileStorageSettingPath(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("FileStorageSettings:TempProofPath") ?? "";
        }

        public static void EnsureDirectoryExists(this IConfiguration configuration)
        {
            var path = configuration.FileStorageSettingPath();
            var currentDirectory = Directory.GetCurrentDirectory();  

            if (!string.IsNullOrEmpty(path) && !Path.IsPathRooted(path))
                path = Path.Combine(currentDirectory, path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
