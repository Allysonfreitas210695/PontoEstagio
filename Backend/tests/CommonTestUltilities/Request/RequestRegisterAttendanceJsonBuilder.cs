using Bogus;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Communication.Request;

namespace CommonTestUltilities.Request;

public class RequestRegisterAttendanceJsonBuilder
{
    public static RequestRegisterAttendanceJson Build()
    {
        return new Faker<RequestRegisterAttendanceJson>()
            .RuleFor(x => x.Date, f => DateTime.Now.Date)
            .RuleFor(x => x.CheckIn, f => new TimeSpan(f.Random.Int(7, 10), f.Random.Int(0, 59), 0)) 
            .RuleFor(x => x.CheckOut, (f, x) =>
            {
                var checkOutHour = x.CheckIn.Hours + f.Random.Int(1, 5); 
                var checkOutMinute = f.Random.Int(0, 59);
                return new TimeSpan(Math.Min(checkOutHour, 23), checkOutMinute, 0);
            })
            .RuleFor(x => x.Status, f => f.PickRandom<AttendanceStatus>())
            .RuleFor(x => x.ProofImageBase64, f => f.Image.PicsumUrl(200,200))
            .Generate();
    }
}
