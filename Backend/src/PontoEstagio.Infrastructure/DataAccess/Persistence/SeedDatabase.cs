using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PontoEstagio.Infrastructure.Context;
using Bogus.Extensions.Brazil;
using PontoEstagio.Domain.Security.Cryptography;

namespace PontoEstagio.Infrastructure.DataAccess.Persistence;

public static class SeedDatabaseInitial
{

    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<PontoEstagioDbContext>();

        if(!await dbContext.Users.AsNoTracking().AnyAsync(x => x.Type == UserType.Admin))
            await SeedAdminUser(dbContext);

        if (!await dbContext.EmailTemplates.AsNoTracking().AnyAsync())
            await SeedEmailTemplates(dbContext);

        if (await dbContext.Users.AnyAsync())
            return;

        await SeedUsers(dbContext);
        await SeedCompanies(dbContext);
        await SeedProjects(dbContext);
        await SeedUserProjectsAndRelatedData(dbContext);
        
    }

    private static async Task SeedAdminUser(PontoEstagioDbContext dbContext)
    {

        var adminUser = new User(
                                    Guid.NewGuid(), 
                                    "Admin", 
                                    Email.Criar("admin@admin.com"), 
                                    UserType.Admin,
                                    BCrypt.Net.BCrypt.HashPassword("!Aa1234567"),
                                    true
                                );
        await dbContext.Users.AddAsync(adminUser);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUsers(PontoEstagioDbContext dbContext)
    {
        var faker = new Faker("pt_BR");
        var users = new List<User>();

        for (int i = 0; i < 3; i++)
        {
            var userType = i % 2 == 0 ? UserType.Intern : UserType.Supervisor;
            var user = new User(
                Guid.NewGuid(),
                faker.Name.FullName(),
                Email.Criar(faker.Internet.Email()),
                userType,
                faker.Internet.Password(prefix: "!Aa1"),
                true
            );
            users.Add(user);
        }

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedCompanies(PontoEstagioDbContext dbContext)
    {
        var faker = new Faker("pt_BR");
        var companies = new List<Company>();

        for (int i = 0; i < 2; i++)
        {
            var company = new Company(
                Guid.NewGuid(),
                faker.Company.CompanyName(),
                faker.Company.Cnpj(),
                faker.Phone.PhoneNumber(),
                Email.Criar(faker.Internet.Email())
            );

            companies.Add(company);
        }

        await dbContext.Companies.AddRangeAsync(companies);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedProjects(PontoEstagioDbContext dbContext)
    {
        var supervisors = await dbContext.Users
                                          .AsNoTracking()
                                          .Where(u => u.Type == UserType.Supervisor)
                                          .ToListAsync();

        if (!supervisors.Any())
            throw new InvalidOperationException("Nenhum supervisor encontrado para atribuir ao projeto.");

        var companies = await dbContext.Companies.AsNoTracking().ToListAsync();

        if (!companies.Any())
            throw new InvalidOperationException("Nenhuma empresa encontrada para associar ao projeto.");

        var faker = new Faker("pt_BR");
        var projects = new List<Project>();

        for (int i = 0; i < 3; i++)
        {
            var supervisor = faker.PickRandom(supervisors);
            var company = faker.PickRandom(companies); 

            var project = new Project(
                Guid.NewGuid(),
                company.Id,
                faker.Commerce.ProductName(),
                faker.Lorem.Sentence(),
                faker.Random.Number(200, 480),
                faker.PickRandom<ProjectStatus>(),
                faker.Date.Past(1, DateTime.UtcNow),
                faker.Date.Soon(3, DateTime.UtcNow),
                supervisor.Id
            );

            projects.Add(project);
        }

        await dbContext.Projects.AddRangeAsync(projects);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUserProjectsAndRelatedData(PontoEstagioDbContext dbContext)
    {
        var faker = new Faker("pt_BR");

        var users = await dbContext.Users.AsNoTracking().ToListAsync();
        var projects = await dbContext.Projects.AsNoTracking().ToListAsync();

        var userProjects = new List<UserProject>();
        var attendances = new List<Attendance>();
        var activities = new List<Activity>();

        foreach (var project in projects)
        {
            var intern = users.FirstOrDefault(u => u.Type == UserType.Intern);
            var supervisor = users.FirstOrDefault(u => u.Type == UserType.Supervisor);

            if (intern != null && supervisor != null)
            {
                var userProjectIntern = new UserProject(null, intern.Id, project.Id, UserType.Intern);
                userProjects.Add(userProjectIntern);

                var userProjectSupervisor = new UserProject(null, supervisor.Id, project.Id, UserType.Supervisor);
                userProjects.Add(userProjectSupervisor);

                var attendance = new Attendance(
                    Guid.NewGuid(),
                    intern.Id,
                    DateTime.Today,
                    new TimeSpan(8, 0, 0),
                    new TimeSpan(17, 0, 0),
                    faker.PickRandom<AttendanceStatus>(),
                    faker.Image.PicsumUrl(200, 200)
                );
                attendances.Add(attendance);

                var activity = new Activity(
                    Guid.NewGuid(),
                    attendance.Id,
                    intern.Id,
                    project.Id,
                    faker.Lorem.Sentence(),
                    DateTime.Now
                );
                activities.Add(activity);
            }
        }

        await dbContext.UserProjects.AddRangeAsync(userProjects);
        await dbContext.Attendances.AddRangeAsync(attendances);
        await dbContext.Activitys.AddRangeAsync(activities);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedEmailTemplates(PontoEstagioDbContext dbContext)
    {
        

        var verificationCodeTemplate = new EmailTemplates
        {
            Title = "Redefinição de Senha",
            Subject = "Código de verificação para redefinição de senha",
            Body = @"
            <h2>Olá, {{UserName}}!</h2>
            <p>Recebemos uma solicitação para redefinir sua senha.</p>
            <p>Seu código de verificação é: <strong>{{VerificationCode}}</strong></p>
            <p>Use este código no aplicativo para confirmar sua identidade e redefinir sua senha.</p>
            <p><strong>O código é válido por até 3 minutos</strong> e deve ser usado apenas por você.</p>
            <p>Se você não solicitou isso, ignore este e-mail ou entre em contato conosco.</p>
            <br />
            <p>Atenciosamente,<br />Equipe Ponto Estágio</p>"
        };

        dbContext.EmailTemplates.Add(verificationCodeTemplate);
        await dbContext.SaveChangesAsync();
    }
}
