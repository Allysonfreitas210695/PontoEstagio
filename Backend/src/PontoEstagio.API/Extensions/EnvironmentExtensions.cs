namespace PontoEstagio.API.Extensions;

 public static class EnvironmentExtensions
{
    public static bool IsTestEnvironment(this IConfiguration configuration)
    {
        return configuration["Environment"] == "Test";
    }
}