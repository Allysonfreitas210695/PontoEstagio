using Bogus;
using Bogus.Extensions.Brazil;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.ValueObjects;

namespace CommonTestUltilities.Entities;

public class CompanyBuilder
{ 
    public static Company Build(
        Guid? id = null,
        string? name = null, 
        string? cnpj = null,  
        string? phone = null, 
        string? email = null,
        Address? address = null
       )
    {
        var faker = new Faker("pt_BR");

        var fakeAddress = address ?? new Address(
                faker.Address.StreetName(),
                faker.Address.BuildingNumber(),
                faker.Address.County(),
                faker.Address.City(),
                faker.Address.StateAbbr(),
                faker.Address.ZipCode("#####-###"),
                faker.Address.SecondaryAddress()
            );

        var _email = email ?? faker.Internet.Email();

        return new Company(
            id: id ?? Guid.NewGuid(),
            name: name ?? faker.Company.CompanyName(),
            cnpj: cnpj ?? faker.Company.Cnpj(),
            phone: phone ?? faker.Phone.PhoneNumber(),
            email: Email.Criar(_email),
            address: address ?? fakeAddress
        );
    } 
}