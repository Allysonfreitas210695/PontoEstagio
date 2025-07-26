using CommonTestUltilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Infrastructure.Context;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private PontoEstagio.Domain.Entities.User _user;
    private string _password;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment("Test")
            .ConfigureAppConfiguration((context, configBuilder) =>
             {
                 configBuilder.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);
             })
            .ConfigureServices(services =>
            {
                var provider = services
                                    .AddEntityFrameworkInMemoryDatabase()
                                    .BuildServiceProvider();

                services.AddDbContext<PontoEstagioDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<PontoEstagioDbContext>();
                var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();

                dbContext.Database.EnsureCreated();

                StartDatabase(dbContext, passwordEncrypter);
            });
    }

    public string GetName() => _user.Name;
    public string GetEmail() => _user.GetEmail(); 
    public string GetPassword() => _password;

    private void StartDatabase(PontoEstagioDbContext dbContext, IPasswordEncrypter passwordEncrypter)
    {
        _user = UserBuilder.Build();
        _password = _user.Password;

        var encryptedPassword = passwordEncrypter.Encrypt(_user.Password);
        _user.UpdatePassword(encryptedPassword);

        dbContext.Users.Add(_user);
        dbContext.SaveChanges();
    }
}
