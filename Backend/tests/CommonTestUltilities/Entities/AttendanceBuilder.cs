using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Enum;

namespace CommonTestUltilities.Entities;

public class AttendanceBuilder
{
    public static Attendance Build(
        Guid? id = null,
        Guid? userId = null, 
        Guid? projectId = null, 
        DateTime? date = null,  
        TimeSpan? checkIn = null, 
        TimeSpan? checkOut = null, 
        AttendanceStatus? status = null,
        string? proofImageBase64 = null
    )
    {
        var faker = new Faker();

        var fakeCheckIn = checkIn ?? new TimeSpan(faker.Random.Int(7, 9), faker.Random.Int(0, 59), 0);
        var fakeCheckOut = checkOut ?? fakeCheckIn.Add(new TimeSpan(faker.Random.Int(4, 8), 0, 0)); 

        return new Attendance(
            id: id ?? Guid.NewGuid(),
            userId: userId ?? Guid.NewGuid(),
            projectId: projectId ?? Guid.NewGuid(),
            date: date ?? DateTime.Now.Date,
            checkIn: fakeCheckIn,
            checkOut: fakeCheckOut,
            status: status ?? faker.PickRandom<AttendanceStatus>(),
            proofImageBase64: proofImageBase64 ?? faker.Image.PicsumUrl(200, 200)
        );
    }
}
