using Bogus;
using Bogus.Extensions.Brazil;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;

namespace CommonTestUltilities.Entities;

public class UserBuilder
{
    public static User Build(
        Guid? id = null,
        Guid? universityId = null,
        Guid? courseId = null,
        string? name = null,
        string? registration = null,
        Email? email = null,
        UserType? type = null,
        string? password = null,
        string? phone = null,
        string? cpf = null,
        string? department = null,
        bool isActive = true)
    {
        var faker = new Faker("pt_BR");

        var user = new User(
            id: id,
            universityId: universityId ?? Guid.NewGuid(),
            courseId: courseId ?? Guid.NewGuid(),
            name: name ?? faker.Name.FullName(),
            registration: registration ?? faker.Random.AlphaNumeric(8),
            email: email ?? Email.Criar(faker.Internet.Email()),
            type: type ?? UserType.Intern,
            password: password ?? faker.Internet.Password(8),
            phone: phone ?? faker.Phone.PhoneNumber("###########"),
            cpf: cpf ?? faker.Person.Cpf(false),
            department: department ?? faker.Commerce.Department()
        );

        if (isActive)
            user.Activate();
        else
            user.Inactivate();

        return user;
    }
}