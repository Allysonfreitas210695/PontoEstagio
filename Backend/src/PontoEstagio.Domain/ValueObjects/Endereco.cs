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
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required.");
        if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Number is required.");
        if (string.IsNullOrWhiteSpace(district)) throw new ArgumentException("District is required.");
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State is required.");
        if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException("Zip code is required.");
        if (zipCode.Length != 8) throw new ArgumentException("Zip code must be 8 characters.");

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
