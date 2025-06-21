

using Microsoft.Extensions.DependencyInjection;
using PontoEstagio.Application.UseCases.Activity.ActivitiesByProject;
using PontoEstagio.Application.UseCases.Activity.Create;
using PontoEstagio.Application.UseCases.Activity.GetActivitiesByAttendanceId;
using PontoEstagio.Application.UseCases.Activity.GetActivitiesByUser;
using PontoEstagio.Application.UseCases.Activity.GetActivityById;
using PontoEstagio.Application.UseCases.Attendance.GetAllAttendances;
using PontoEstagio.Application.UseCases.Attendance.GetAttendanceById;
using PontoEstagio.Application.UseCases.Attendance.Register;
using PontoEstagio.Application.UseCases.Attendance.UpdateStatus;
using PontoEstagio.Application.UseCases.Auth.ForgotPassword;
using PontoEstagio.Application.UseCases.Auth.Refresh;
using PontoEstagio.Application.UseCases.Auth.ResetPassword;
using PontoEstagio.Application.UseCases.Company.DeleteLegalRepresentative;
using PontoEstagio.Application.UseCases.Company.GetAllCompany;
using PontoEstagio.Application.UseCases.Company.GetAllRepresentativesFromCompany;
using PontoEstagio.Application.UseCases.Company.GetCompanyById;
using PontoEstagio.Application.UseCases.Company.GetLegalRepresentativeById;
using PontoEstagio.Application.UseCases.Company.Register;
using PontoEstagio.Application.UseCases.Company.RegisterLegalRepresentative;
using PontoEstagio.Application.UseCases.Company.Update;
using PontoEstagio.Application.UseCases.Company.UpdateLegalRepresentative;
using PontoEstagio.Application.UseCases.Cource.GetAllCources;
using PontoEstagio.Application.UseCases.Cource.GetCourceById;
using PontoEstagio.Application.UseCases.Cource.Register;
using PontoEstagio.Application.UseCases.Cource.Update;
using PontoEstagio.Application.UseCases.Login.DoLogin;
using PontoEstagio.Application.UseCases.Projects.AssignUserToProject;
using PontoEstagio.Application.UseCases.Projects.DeleteUserFromProject;
using PontoEstagio.Application.UseCases.Projects.GetAllProjects;
using PontoEstagio.Application.UseCases.Projects.GetProjectById;
using PontoEstagio.Application.UseCases.Projects.Register;
using PontoEstagio.Application.UseCases.Projects.Update;
using PontoEstagio.Application.UseCases.Projects.UpdateStatus;
using PontoEstagio.Application.UseCases.Reports.Monthly;
using PontoEstagio.Application.UseCases.University.GetAllUniversities;
using PontoEstagio.Application.UseCases.University.GetUniversityById;
using PontoEstagio.Application.UseCases.University.Register;
using PontoEstagio.Application.UseCases.University.Update;
using PontoEstagio.Application.UseCases.Users.CheckUserExists;
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
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();

        services.AddScoped<ICheckUserExistsUseCase, CheckUserExistsUseCase>();
        services.AddScoped<IActivateUserUseCase, ActivateUserUseCase>();
        services.AddScoped<IRegisterActivityUseCase, RegisterActivityUseCase>();
        services.AddScoped<IDeactivatedUserUseCase, DeactivatedUserUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

        services.AddScoped<IGetActivityByIdUseCase, GetActivityByIdUseCase>();
        services.AddScoped<IGetActivitiesByAttendanceIdUseCase, GetActivitiesByAttendanceIdUseCase>();
        services.AddScoped<IGetActivitiesByProjectUseCase, GetActivitiesByProjectUseCase>();
        services.AddScoped<IGetActivitiesByUserUseCase, GetActivitiesByUserUseCase>();

        services.AddScoped<IGetAllAttendancesUseCase, GetAllAttendancesUseCase>();
        services.AddScoped<IRegisterAttendanceUseCase, RegisterAttendanceUseCase>();
        services.AddScoped<IGetAttendanceByIdUseCase, GetAttendanceByIdUseCase>();
        services.AddScoped<IUpdateAttendanceStatusUseCase, UpdateAttendanceStatusUseCase>();

        services.AddScoped<IAssignUserToProjectUseCase, AssignUserToProjectUseCase>();
        services.AddScoped<IDeleteUserFromProjectUseCase, DeleteUserFromProjectUseCase>();

        services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
        services.AddScoped<IGetAllCompanyUseCase, GetAllCompanyUseCase>();
        services.AddScoped<IGetCompanyByIdUseCase, GetCompanyByIdUseCase>();
        services.AddScoped<ICompanyUpdateUseCase, CompanyUpdateUseCase>();
        
        services.AddScoped<IRegisterLegalRepresentativeUseCase, RegisterLegalRepresentativeUseCase>();
        services.AddScoped<IGetAllRepresentativesFromCompanyUseCase, GetAllRepresentativesFromCompanyUseCase>();
        services.AddScoped<IGetLegalRepresentativeByIdUseCase, GetLegalRepresentativeByIdUseCase>();
        services.AddScoped<IUpdateLegalRepresentativeUseCase, UpdateLegalRepresentativeUseCase>();
        services.AddScoped<IDeleteLegalRepresentativeUseCase, DeleteLegalRepresentativeUseCase>();

        services.AddScoped<IGetAllCourcesUseCase, GetAllCourcesUseCase>();
        services.AddScoped<IGetCourceByIdUseCase, GetCourceByIdUseCase>();
        services.AddScoped<IRegisterCourceUseCase, RegisterCourceUseCase>();
        services.AddScoped<IUpdateCourceUseCase, UpdateCourceUseCase>();

        services.AddScoped<IGetAllProjectsUseCase, GetAllProjectsByUserUseCase>();
        services.AddScoped<IGetProjectByIdUseCase, GetProjectByIdUseCase>();
        services.AddScoped<IRegisterProjectUseCase, RegisterProjectUseCase>();
        services.AddScoped<IUpdateProjectUseCase, UpdateProjectUseCase>();
        services.AddScoped<IUpdateProjectStatusUseCase, UpdateProjectStatusUseCase>();

        services.AddScoped<IGetAllUniversitiesUseCase, GetAllUniversitiesUseCase>();
        services.AddScoped<IGetUniversityByIdUseCase, GetUniversityByIdUseCase>();
        services.AddScoped<IRegisterUniversityUseCase, RegisterUniversityUseCase>();
        services.AddScoped<IUpdateUniversityUseCase, UpdateUniversityUseCase>();

        services.AddScoped<IReportsMonthlyUseCase, ReportsMonthlyUseCase>();
    }
}