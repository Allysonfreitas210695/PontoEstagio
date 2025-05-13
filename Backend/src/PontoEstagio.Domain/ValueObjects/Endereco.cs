using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.ValueObjects;

public class Address
{
    public string Street { get; }
    public string Number { get; }
    public string District { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Complement { get; }

    public Address(string street, string number, string district, string city, string state, string zipCode, string complement = "")
    {
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException(ErrorMessages.Address_StreetRequired);
        if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException(ErrorMessages.Address_NumberRequired);
        if (string.IsNullOrWhiteSpace(district)) throw new ArgumentException(ErrorMessages.Address_DistrictRequired);
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(ErrorMessages.Address_CityRequired);
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException(ErrorMessages.Address_StateRequired);
        if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException(ErrorMessages.Address_ZipCodeRequired);
        if (zipCode.Length != 8) throw new ArgumentException(ErrorMessages.Address_ZipCodeInvalid);

        Street = street;
        Number = number;
        District = district;
        City = city;
        State = state;
        ZipCode = zipCode;
        Complement = complement ?? string.Empty;
    }
    
    public override string ToString()
    {
        return $"{Street}, {Number} - {District}, {City} - {State}, {ZipCode}" +
               (!string.IsNullOrEmpty(Complement) ? $" ({Complement})" : "");
    }
}
