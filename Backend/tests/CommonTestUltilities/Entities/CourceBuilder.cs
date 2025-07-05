using Bogus;
using PontoEstagio.Domain.Entities;

namespace CommonTestUltilities.Entities;
public class CourceBuilder
{
    public static Course Build(
       Guid? id = null,
       string? name = null,
       int? workloadHours = null,
       Guid? universityId = null
   )
    {
        var faker = new Faker();

        return new Course(
            id: id ?? Guid.NewGuid(),
            name: name ?? $"{faker.Commerce.Department()} - {faker.Random.Word()}",
            workloadHours: workloadHours ?? faker.Random.Int(40, 1000),
            universityId: universityId ?? Guid.NewGuid()
        );
    }
}
