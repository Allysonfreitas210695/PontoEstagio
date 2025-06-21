using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PontoEstagio.Infrastructure.Context;
using Bogus.Extensions.Brazil; 

namespace PontoEstagio.Infrastructure.DataAccess.Persistence;

public static class SeedDatabaseInitial
{

    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<PontoEstagioDbContext>();

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {

            if (!await dbContext.EmailTemplates.AsNoTracking().AnyAsync())
                await SeedEmailTemplates(dbContext);

            if (!await dbContext.Universities.AnyAsync())
                await SeedUniversities(dbContext);
                
            if (!await dbContext.Courses.AsNoTracking().AnyAsync())
                await SeedCourses(dbContext);

            if (await dbContext.Users.AnyAsync())
                return;

            if (!await dbContext.Users.AsNoTracking().AnyAsync(x => x.Type == UserType.Admin))
                await SeedAdminUser(dbContext);

            await SeedUsers(dbContext);

            await SeedCompanies(dbContext);
            await SeedLegalRepresentatives(dbContext);

            await SeedProjects(dbContext);
            await SeedUserProjectsAndRelatedData(dbContext);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }


    private static async Task SeedAdminUser(PontoEstagioDbContext dbContext)
    {
        var universities = await dbContext.Universities.AsNoTracking().ToListAsync();
        if (!universities.Any())
            throw new InvalidOperationException("Nenhuma universidade encontrada para associar ao projeto.");

        var courses = await dbContext.Courses.AsNoTracking().ToListAsync();
        if (!courses.Any())
            throw new InvalidOperationException("Nenhum curso encontrado para associar ao projeto.");

        var faker = new Faker("pt_BR");

        var university = faker.PickRandom(universities);
        var course = faker.PickRandom(courses);

        var adminUser = new User(
                                    Guid.NewGuid(),
                                    university.Id,
                                    course.Id,
                                    "Admin", 
                                    new Random().Next(100000, 999999).ToString(),
                                    Email.Criar("admin@admin.com"), 
                                    UserType.Admin,
                                    BCrypt.Net.BCrypt.HashPassword("!Aa1234567"),
                                    faker.Phone.PhoneNumber()
                                );
        await dbContext.Users.AddAsync(adminUser);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUsers(PontoEstagioDbContext dbContext)
    {
        var faker = new Faker("pt_BR");
        var users = new List<User>();

        var universities = await dbContext.Universities.AsNoTracking().ToListAsync();
        if (!universities.Any())
            throw new InvalidOperationException("Nenhuma universidade encontrada para associar ao projeto.");

        var university = faker.PickRandom(universities);

         var courses = await dbContext.Courses.AsNoTracking().ToListAsync();
        if (!courses.Any())
            throw new InvalidOperationException("Nenhum curso encontrado para associar ao projeto.");

        var course = faker.PickRandom(courses);

        for (int i = 0; i < 3; i++)
        {
            var userType = i % 2 == 0 ? UserType.Intern : UserType.Supervisor;
            var user = new User(
                Guid.NewGuid(),
                university.Id,
                course.Id,
                new Random().Next(100000, 999999).ToString(),
                faker.Name.FullName(),
                Email.Criar(faker.Internet.Email()),
                userType,
                faker.Internet.Password(prefix: "!Aa1"),
                faker.Phone.PhoneNumber()
            );
            users.Add(user);
        }

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUniversities(PontoEstagioDbContext dbContext)
    { 
        var faker = new Faker("pt_BR");
        var universities = new List<University>();

        for (int i = 0; i < 2; i++)
        {
            var name = faker.Company.CompanyName();
            var acronym = new string(name
                    .Where(char.IsUpper)
                    .DefaultIfEmpty(name[0])
                    .ToArray());

            var cnpj = faker.Company.Cnpj();
            var phone = faker.Phone.PhoneNumber();
            var email = Email.Criar(faker.Internet.Email());

            var address = new Address(
                street: faker.Address.StreetName(),
                number: faker.Address.BuildingNumber(),
                district: faker.Address.County(),
                city: faker.Address.City(),
                state: faker.Address.StateAbbr(),
                zipCode: faker.Address.ZipCode("#####-###"),
                complement: faker.Address.SecondaryAddress()
            );

            var university = new University(
                id: Guid.NewGuid(),
                name: name,
                acronym: acronym,
                cnpj: cnpj,
                phone: phone,
                email: email,
                address: address,
                isActive: true
            );

            universities.Add(university);
        }

        await dbContext.Universities.AddRangeAsync(universities);
        await dbContext.SaveChangesAsync(); 
    }

    private static async Task SeedCourses(PontoEstagioDbContext dbContext)
    {
        var faker = new Faker("pt_BR");

        var universities = await dbContext.Universities.AsNoTracking().ToListAsync();
        if (!universities.Any())
            throw new InvalidOperationException("Nenhuma universidade encontrada para associar ao projeto.");

        var university = faker.PickRandom(universities);


        var courses = new List<Course>();

        var courseNames = new[] {
            "Engenharia de Software", "Administração", "Direito", "Psicologia",
            "Medicina", "Arquitetura", "Ciência da Computação", "Pedagogia"
        };

        for (int i = 0; i < 2; i++)
        {
            var course = new Course(
                Guid.NewGuid(),
                faker.PickRandom(courseNames),
                faker.Random.Number(1, 10),
                university.Id
            );

            courses.Add(course);
        }

        await dbContext.Courses.AddRangeAsync(courses);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedLegalRepresentatives(PontoEstagioDbContext dbContext)
    {
        var faker = new Faker("pt_BR");
        var representatives = new List<LegalRepresentative>();

        var companies = await dbContext.Companies.ToListAsync();

        foreach (var company in companies)
        {
            var representative = new LegalRepresentative(
                id: null,
                fullName: faker.Name.FullName(),
                cpf: faker.Person.Cpf(false),
                position: faker.Name.JobTitle(),
                email: Email.Criar(faker.Internet.Email()),
                companyId: company.Id
            );

            representatives.Add(representative);
        }

        await dbContext.LegalRepresentatives.AddRangeAsync(representatives);
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
                Email.Criar(faker.Internet.Email()),
                new Address(
                    faker.Address.StreetName(),           
                    faker.Random.Number(1, 9999).ToString(), 
                    faker.Address.County(),              
                    faker.Address.City(),             
                    faker.Address.StateAbbr(),           
                    faker.Address.ZipCode("#####-###"),   
                    faker.Address.SecondaryAddress()      
                )
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

        var universities = await dbContext.Universities.AsNoTracking().ToListAsync();
        if (!universities.Any())
            throw new InvalidOperationException("Nenhuma universidade encontrada para associar ao projeto.");

        var faker = new Faker("pt_BR");
        var projects = new List<Project>();

        for (int i = 0; i < 3; i++)
        {
            var supervisor = faker.PickRandom(supervisors);
            var company = faker.PickRandom(companies); 
            var university = faker.PickRandom(universities); 

            var project = new Project(
                Guid.NewGuid(),
                company.Id,
                university.Id,
                faker.Commerce.ProductName(),
                faker.Lorem.Sentence(),
                faker.Random.Number(200, 480),
                faker.PickRandom<ProjectStatus>(),
                faker.Date.Past(1, DateTime.UtcNow),
                faker.Date.Soon(3, DateTime.UtcNow),
                supervisor.Id,
                faker.PickRandom<ProjectClassification>()
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

        foreach (var project in projects)
        {
            var intern = users.FirstOrDefault(u => u.Type == UserType.Intern);
            var supervisor = users.FirstOrDefault(u => u.Type == UserType.Supervisor);

            if (intern != null && supervisor != null)
            {
                var userProjectIntern = new UserProject(null, intern.Id, project.Id, UserType.Intern);
                var userProjectSupervisor = new UserProject(null, supervisor.Id, project.Id, UserType.Supervisor);

                userProjects.Add(userProjectIntern);
                userProjects.Add(userProjectSupervisor);
            }
        }

        await dbContext.UserProjects.AddRangeAsync(userProjects);
        await dbContext.SaveChangesAsync();

        var savedUserProjects = await dbContext.UserProjects.AsNoTracking().ToListAsync();

        try
        {
            foreach (var up in savedUserProjects.Where(up => up.Role == UserType.Intern))
            {
                var attendance = new Attendance(
                    Guid.NewGuid(),
                    up.UserId,
                    up.ProjectId,
                    DateTime.UtcNow,
                    new TimeSpan(8, 0, 0),
                    new TimeSpan(17, 0, 0),
                    faker.PickRandom<AttendanceStatus>(),
                    faker.Image.PicsumUrl(200, 200)
                );
                attendances.Add(attendance);
            }

            await dbContext.Attendances.AddRangeAsync(attendances);
            await dbContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Console.Write(ex?.InnerException?.Message ?? ex?.Message);
        }

        var savedAttendances = await dbContext.Attendances.AsNoTracking().ToListAsync();

        var activities = new List<Activity>();
        foreach (var attendance in savedAttendances)
        {
            var activity = new Activity(
                Guid.NewGuid(),
                attendance.Id,
                attendance.UserId,
                faker.Lorem.Sentence(),
                DateTime.UtcNow
            );
            activities.Add(activity);
        }
 
        await dbContext.Activitys.AddRangeAsync(activities);
        await dbContext.SaveChangesAsync(); 
    }

    private static async Task SeedEmailTemplates(PontoEstagioDbContext dbContext)
    {
        var resetPasswordTemplate = new EmailTemplates
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

        var coordinatorVerificationTemplate = new EmailTemplates
        {
            Title = "Cadastro de Coordenação",
            Subject = "Código de Verificação - Cadastro de Coordenação",
            Body = @"
                <h2>Olá!</h2>
                <p>Para concluir o cadastro da coordenação, utilize o seguinte código de verificação:</p>
                <p><strong>Código: {{VerificationCode}}</strong></p>
                <p>Este código é necessário para validar a criação de uma conta vinculada à universidade no sistema PontoEstágio.</p>
                <p>Caso você não tenha solicitado este cadastro, ignore esta mensagem.</p>
                <br />
                <p>Atenciosamente,<br />Equipe PontoEstágio</p>"
        };

        dbContext.EmailTemplates.AddRange(resetPasswordTemplate, coordinatorVerificationTemplate);
        await dbContext.SaveChangesAsync();
    }
}
