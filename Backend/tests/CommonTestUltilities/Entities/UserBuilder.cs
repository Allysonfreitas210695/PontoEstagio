using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;

namespace CommonTestUltilities.Entities;

public class UserBuilder
{
    public static User Build(
        Guid? id = null, 
        string? name = null,
        Email? email = null, 
        UserType? type = null, 
        string? password = null, 
        bool? isActive = null
    )
    {
        var faker = new Faker();

        return new User(
            id ?? Guid.NewGuid(),
            name ?? faker.Name.FullName(),
            email ?? Email.Criar(faker.Internet.Email()),
            type ?? faker.PickRandom<UserType>(),
            password ?? faker.Internet.Password(12),
            isActive ?? faker.Random.Bool()
        );
    } 
}
