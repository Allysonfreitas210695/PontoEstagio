using Bogus;
using PontoEstagio.Domain.Entities;

namespace CommonTestUltilities.Entities;

public static class PasswordRecoveryBuilder
{
    public static PasswordRecovery Build(
        Guid? id = null,
        Guid? userId = null,
        string? code = null
    )
    {
        var faker = new Faker();

        return new PasswordRecovery(
            id: id ?? Guid.NewGuid(),
            userId: userId ?? Guid.NewGuid(),
            code: code ?? faker.Random.AlphaNumeric(6)  
        );
    }
}
