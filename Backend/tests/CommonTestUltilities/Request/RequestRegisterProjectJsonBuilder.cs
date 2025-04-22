using Bogus;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Communication.Request; 
namespace CommonTestUltilities.Request;

public class RequestRegisterProjectJsonBuilder
{
    public static RequestRegisterProjectJson Build()
    {
        return new Faker<RequestRegisterProjectJson>()
            .RuleFor(x => x.CompanyId, f => f.Random.Guid())
            .RuleFor(x => x.Name, f => f.Company.CatchPhrase())
            .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
            .RuleFor(x => x.Status, f => f.PickRandom<ProjectStatus>())
            .RuleFor(x => x.TotalHours, f => f.Random.Long(100, 1000))
            .RuleFor(x => x.StartDate, f => f.Date.FutureOffset(1).DateTime.Date)
            .RuleFor(x => x.EndDate, (f, x) => f.Date.Between(x.StartDate.AddDays(1), x.StartDate.AddMonths(6)))
            .Generate();
    }
}
