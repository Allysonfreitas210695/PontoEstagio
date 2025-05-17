using Bogus;
using PontoEstagio.Communication.Request;

namespace CommonTestUltilities.Request;

public class RequestLoginUserJsonBuilder
{
    public static RequestLoginUserJson Build()
    {
        return new Faker<RequestLoginUserJson>()
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "!Aa1")) 
            .Generate();
    }
}