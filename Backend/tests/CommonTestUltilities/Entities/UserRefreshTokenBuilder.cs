using Bogus;
using PontoEstagio.Domain.Entities;

namespace CommonTestUltilities.Entities;

public class UserRefreshTokenBuilder
{
    public static UserRefreshToken Build(
        Guid? id = null, 
        Guid? userId = null, 
        string? token = null, 
        DateTime? expirationDate = null
    )
    {
        var faker = new Faker();

        return new UserRefreshToken(
            id: id ?? Guid.NewGuid(),
            userId: userId ?? Guid.NewGuid(),   
            token: token ?? faker.Random.String2(10),   
            expirationDate: expirationDate ?? DateTime.Now.AddDays(1).Date
        );
    }
}