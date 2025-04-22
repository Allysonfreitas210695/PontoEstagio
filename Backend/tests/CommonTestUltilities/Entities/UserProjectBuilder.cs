using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;

namespace CommonTestUltilities.Entities;

public class UserProjectBuilder
{
    public static UserProject Build(
        Guid? id = null, 
        Guid? userId = null, 
        Guid? projectId = null, 
        UserType? role = null
    )
    {
        var faker = new Faker();

        return new UserProject(
            id: id ?? Guid.NewGuid(),
            userId: userId ?? Guid.NewGuid(),   
            projectId: projectId ?? Guid.NewGuid(),   
            role: role ?? faker.PickRandom<UserType>() 
        );
    }
}
