using Bogus;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Communication.Request;
using System;
using System.Net.NetworkInformation;

namespace CommonTestUltilities.Request
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build()
        {
            var faker = new Faker();

            return new Faker<RequestRegisterUserJson>()
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Registration, f => f.Random.AlphaNumeric(10).ToUpper())
                .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "!Aa"))
                .RuleFor(x => x.UniversityId, f => Guid.NewGuid())
                .RuleFor(x => x.CourseId, (f, x) =>
                {
                    if (x.Type == UserType.Intern || x.Type == UserType.Coordinator)
                        return Guid.NewGuid();
                    else
                        return null;
                })
                .RuleFor(x => x.isActive, f => f.Random.Bool())
                .RuleFor(x => x.Type, f => f.Random.Enum<UserType>())
                .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
                .Generate();
        }
    }
}
