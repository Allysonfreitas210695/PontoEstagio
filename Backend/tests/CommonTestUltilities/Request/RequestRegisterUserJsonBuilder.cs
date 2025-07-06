using Bogus;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Communication.Request;
using System;

namespace CommonTestUltilities.Request;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(UserType? userType = null)
    {
        var faker = new Faker("pt_BR");
        var type = userType ?? faker.PickRandom<UserType>();

        var request = new RequestRegisterUserJson
        {
            Name = faker.Name.FullName(),
            Email = faker.Internet.Email(),
            Password = "ValidPass123!", // Senha que atende ao PasswordValidator
            UniversityId = Guid.NewGuid(),
            isActive = true,
            Type = type
        };

        // Configurações específicas por tipo
        switch (type)
        {
            case UserType.Intern:
                request.CourseId = Guid.NewGuid();
                request.Registration = faker.Random.AlphaNumeric(10).ToUpper();
                request.Phone = faker.Phone.PhoneNumber("###########");
                break;

            case UserType.Coordinator:
                request.CourseId = Guid.NewGuid();
                request.Registration = faker.Random.AlphaNumeric(10).ToUpper();
                request.Phone = faker.Phone.PhoneNumber("###########");
                break;

            case UserType.Advisor:
                request.CPF = faker.Random.ReplaceNumbers("###########");
                request.Department = faker.Commerce.Department();
                break;

            case UserType.Supervisor:
                // Nenhum campo adicional obrigatório
                break;
        }

        return request;
    }

    // Métodos adicionais para cenários específicos
    public static RequestRegisterUserJson BuildIntern()
    {
        return Build(UserType.Intern);
    }

    public static RequestRegisterUserJson BuildCoordinator()
    {
        return Build(UserType.Coordinator);
    }

    public static RequestRegisterUserJson BuildAdvisor()
    {
        return Build(UserType.Advisor);
    }

    public static RequestRegisterUserJson BuildSupervisor()
    {
        return Build(UserType.Supervisor);
    }
}