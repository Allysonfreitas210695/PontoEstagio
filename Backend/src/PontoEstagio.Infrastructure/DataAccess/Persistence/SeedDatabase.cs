using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PontoEstagio.Infrastructure.Context;

namespace PontoEstagio.Infrastructure.DataAccess.Persistence;

public static class SeedDatabaseInitial
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<PontoEstagioDbContext>();

        if (await dbContext.Users.AnyAsync())
            return;

        await SeedUsers(dbContext);
        await SeedProjects(dbContext);
        await SeedUserProjectsAndRelatedData(dbContext);
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

    private static async Task SeedProjects(PontoEstagioDbContext dbContext)
    {
        var supervisors = await dbContext.Users
                                          .AsNoTracking()
                                          .Where(u => u.Type == UserType.Supervisor) 
                                          .ToListAsync();

        if (!supervisors.Any())
        {
            throw new InvalidOperationException("Nenhum supervisor encontrado para atribuir ao projeto.");
        }

        var faker = new Faker("pt_BR");
        var projects = new List<Project>();

        for (int i = 0; i < 3; i++)
        {
            var supervisor = faker.PickRandom(supervisors); 

            var project = new Project(
                faker.Commerce.ProductName(),          
                faker.Lorem.Sentence(),                 
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
                var userProjectIntern = new UserProject(intern.Id, project.Id, UserType.Intern);
                userProjects.Add(userProjectIntern);

                var userProjectSupervisor = new UserProject(supervisor.Id, project.Id, UserType.Supervisor);
                userProjects.Add(userProjectSupervisor);

                var attendance = new Attendance(
                    intern.Id,
                    DateTime.Today,
                    new TimeSpan(8, 0, 0),
                    new TimeSpan(17, 0, 0)
                );
                attendances.Add(attendance);

                var activity = new Activity(
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

}
