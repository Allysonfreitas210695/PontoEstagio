using Bogus;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.ValueObjects; 

namespace CommonTestUltilities.Entities
{
    public class UniversityBuilder
    {
        public static University Build(
            Guid? id = null,
            string? name = null,
            string? acronym = null,
            string? cnpj = null,
            Email? email = null,
            string? phone = null,
            bool? isActive = null,
            Address? address = null
        )
        {
            var faker = new Faker("pt_BR");

            var fakeEmail = email ?? Email.Criar(faker.Internet.Email());

            var fakeAddress = address ?? new Address(
                faker.Address.StreetName(),
                faker.Address.BuildingNumber(),
                faker.Address.County(),
                faker.Address.City(),
                faker.Address.StateAbbr(),
                faker.Address.ZipCode("#####-###"),
                faker.Address.SecondaryAddress()
            );

            return new University(
                id ?? Guid.NewGuid(),
                name ?? faker.Company.CompanyName(),
                acronym ?? faker.Random.AlphaNumeric(5).ToUpper(),
                cnpj ?? "00.000.000/0001-00",
                fakeEmail,
                phone ?? faker.Phone.PhoneNumber("(##) #####-####"),
                isActive ?? true,
                fakeAddress
            );
        }
    }
}
