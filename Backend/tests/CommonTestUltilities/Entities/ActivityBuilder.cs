using Bogus;
using PontoEstagio.Domain.Entities;

namespace CommonTestUltilities.Entities;

public class ActivityBuilder
{
    public static Activity Build(
        Guid? id = null,
        Guid? attendanceId = null,
        Guid? userId = null,
        string? description = null,
        DateTime? recordedAt = null,
        string? proofFilePath = null
    )
    {
        var faker = new Faker(); 

        return new Activity(
            id ?? Guid.NewGuid(),
            attendanceId ?? Guid.NewGuid(),
            userId ?? Guid.NewGuid(),
            description ?? faker.Lorem.Sentence(10),
            recordedAt ?? DateTime.Now.AddDays(-1),
            proofFilePath: proofFilePath
        );
    }
}