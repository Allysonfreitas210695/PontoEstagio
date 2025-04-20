

using Microsoft.Extensions.DependencyInjection;
using PontoEstagio.Application.UseCases.Attendance.GetAllAttendances;
using PontoEstagio.Application.UseCases.Attendance.GetAttendanceById;
using PontoEstagio.Application.UseCases.Attendance.Register;
using PontoEstagio.Application.UseCases.Auth.Refresh;
using PontoEstagio.Application.UseCases.Login.DoLogin;
using PontoEstagio.Application.UseCases.Projects.AssignUserToProject;
using PontoEstagio.Application.UseCases.Projects.DeleteUserFromProject;
using PontoEstagio.Application.UseCases.Projects.GetAllProjects;
using PontoEstagio.Application.UseCases.Projects.GetProjectById;
using PontoEstagio.Application.UseCases.Projects.Register;
using PontoEstagio.Application.UseCases.Projects.Update;
using PontoEstagio.Application.UseCases.Projects.UpdateStatus;
using PontoEstagio.Application.UseCases.Users.Deactivated;
using PontoEstagio.Application.UseCases.Users.Delete;
using PontoEstagio.Application.UseCases.Users.GetAllUsers;
using PontoEstagio.Application.UseCases.Users.GetUserById;
using PontoEstagio.Application.UseCases.Users.Register;
using PontoEstagio.Application.UseCases.Users.Update;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        services.AddScoped<IActivateUserUseCase, ActivateUserUseCase>();
        services.AddScoped<IDeactivatedUserUseCase, DeactivatedUserUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

        services.AddScoped<IGetAllAttendancesUseCase, GetAllAttendancesUseCase>();
        services.AddScoped<IRegisterAttendanceUseCase, RegisterAttendanceUseCase>();
        services.AddScoped<IGetAttendanceByIdUseCase, GetAttendanceByIdUseCase>();

        services.AddScoped<IAssignUserToProjectUseCase, AssignUserToProjectUseCase>();
        services.AddScoped<IDeleteUserFromProjectUseCase, DeleteUserFromProjectUseCase>();

        services.AddScoped<IGetAllProjectsUseCase, GetAllProjectsByUserUseCase>();
        services.AddScoped<IGetProjectByIdUseCase, GetProjectByIdUseCase>();
        services.AddScoped<IRegisterProjectUseCase, RegisterProjectUseCase>();
        services.AddScoped<IUpdateProjectUseCase, UpdateProjectUseCase>();
        services.AddScoped<IUpdateProjectStatusUseCase, UpdateProjectStatusUseCase>();
    }
}