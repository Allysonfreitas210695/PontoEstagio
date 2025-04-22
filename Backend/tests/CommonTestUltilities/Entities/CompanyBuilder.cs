using Bogus;
using Bogus.Extensions.Brazil;
using PontoEstagio.Domain.Entities;

namespace CommonTestUltilities.Entities;

public class CompanyBuilder
{ 
    public static Company Build(
        Guid? id = null,
        string? name = null, 
        string? cnpj = null,  
        string? phone = null, 
        string? email = null
       )
    {
        var faker = new Faker();
        return new Company(
            id: id ?? Guid.NewGuid(),
            name: name ?? faker.Company.CompanyName(),
            cnpj: cnpj ?? faker.Company.Cnpj(),
            phone: phone ?? faker.Phone.PhoneNumber(),
            email: email ?? faker.Internet.Email()
        );
    } 
}