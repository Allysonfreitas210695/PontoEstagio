namespace PontoEstagio.Domain.Helpers;
public class CodeGeneratorHelper
{
    public static string Generate(int length = 6)
    {
        var random = new Random();
        var code = string.Empty;

        for (int i = 0; i < length; i++)
        {
            code += random.Next(0, 10); 
        }

        return code;
    }
}
