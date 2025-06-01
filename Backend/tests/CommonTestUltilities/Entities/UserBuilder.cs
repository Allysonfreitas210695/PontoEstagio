using Bogus;
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
        string? phone = null
    )
    {
        var faker = new Faker();

        return new User(
            id ?? Guid.NewGuid(),
            universityId ?? Guid.NewGuid(),
            courseId ?? Guid.NewGuid(),
            name ?? faker.Name.FullName(),
            registration ?? new Random().Next(100000, 999999).ToString(),
            email ?? Email.Criar(faker.Internet.Email()),
            type ?? faker.PickRandom<UserType>(),
            password ?? faker.Internet.Password(12),
            phone: phone ?? faker.Phone.PhoneNumber()
        );
    } 
}
