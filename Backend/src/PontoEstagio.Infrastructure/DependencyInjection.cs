using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoEstagio.Domain.Repositories;
using PontoEstagio.Domain.Repositories.Activity;
using PontoEstagio.Domain.Repositories.Attendance;
using PontoEstagio.Domain.Repositories.Comapany;
using PontoEstagio.Domain.Repositories.Projects;
using PontoEstagio.Domain.Repositories.Report;
using PontoEstagio.Domain.Repositories.User;
using PontoEstagio.Domain.Repositories.UserProjects;
using PontoEstagio.Domain.Security.Cryptography;
using PontoEstagio.Domain.Security.Token;
using PontoEstagio.Infrastructure.Context;
using PontoEstagio.Infrastructure.DataAccess.Repositories;
using PontoEstagio.Infrastructure.Repositories;
using PontoEstagio.Infrastructure.Security;
using PontoEstagio.Infrastructure.Security.Tokens;
using PontoEstagio.Infrastructure.Services.LoggedUser;

namespace PontoEstagio.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<IPasswordEncrypter, BcryptPasswordEncrypter>();
        
        services.AddScoped<ITokenRefreshToken, TokenRefreshToken>();
        services.AddScoped<ITokenGenerateAccessToken, TokenGenerateAccessToken>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICreateActivityUseCase, CreateActivityUseCase>();

        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    { 
        services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
        
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

        services.AddScoped<IActivityReadOnlyRepository, ActivityRepository>();

        services.AddScoped<IAttendanceReadOnlyRepository, AttendanceRepository>();
        services.AddScoped<IAttendanceWriteOnlyRepository, AttendanceRepository>();
        services.AddScoped<IAttendanceUpdateOnlyRepository, AttendanceRepository>();

        services.AddScoped<ICompanyWriteOnlyRepository, CompanyRepository>();
        services.AddScoped<ICompanyReadOnlyRepository, CompanyRepository>();
        services.AddScoped<ICompanyUpdateOnlyRepository, CompanyRepository>();

        services.AddScoped<IProjectReadOnlyRepository, ProjectRepository>();
        services.AddScoped<IProjectWriteOnlyRepository, ProjectRepository>();
        services.AddScoped<IProjectUpdateOnlyRepository, ProjectRepository>();

        services.AddScoped<IUserProjectsReadOnlyRepository, UserProjectsRepository>();
        services.AddScoped<IUserProjectsWriteOnlyRepository, UserProjectsRepository>();
        services.AddScoped<IUserProjectsUpdateOnlyRepository, UserProjectsRepository>();

        services.AddScoped<IReportReadOnlyRepository, ReportRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException("Connection string 'DefaultConnection' is not configured.");
        services.AddDbContext<PontoEstagioDbContext>(options => options.UseSqlServer(connectionString));
    }
}
