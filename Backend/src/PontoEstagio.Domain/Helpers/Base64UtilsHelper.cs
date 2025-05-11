using System.Text.RegularExpressions;

namespace PontoEstagio.Domain.Helpers;

public static  class Base64UtilsHelper
{
    public static string GetMimeTypeFromBase64(string base64String)
    {
        var match = Regex.Match(base64String, @"^data:(?<mime>.+?);base64,");
        
        if (match.Success)
            return match.Groups["mime"].Value;
        
        return "application/octet-stream";
    } 
}