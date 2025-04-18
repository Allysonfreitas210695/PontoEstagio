using Bogus;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Communication.Request;

namespace CommonTestUltilities.Request;
public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "!Aa1"))
            .RuleFor(x => x.Type, f => f.Random.Enum<UserType>())
            .Generate();
    }
}
