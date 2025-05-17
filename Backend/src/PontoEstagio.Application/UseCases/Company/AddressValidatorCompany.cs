using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Company;

public class AddressValidatorCompany : AbstractValidator<RequestAddressJson>
{
    public AddressValidatorCompany()
    {
        RuleFor(a => a.Street)
            .NotEmpty().WithMessage(ErrorMessages.StreetRequired);

        RuleFor(a => a.Number)
            .NotEmpty().WithMessage(ErrorMessages.NumberRequired);

        RuleFor(a => a.City)
            .NotEmpty().WithMessage(ErrorMessages.CityRequired);

        RuleFor(a => a.State)
            .NotEmpty().WithMessage(ErrorMessages.StateRequired)
            .Length(2).WithMessage(ErrorMessages.StateInvalidLength);

        RuleFor(a => a.ZipCode)
            .NotEmpty().WithMessage(ErrorMessages.ZipCodeRequired)
            .Matches(@"^\d{5}-\d{3}$").WithMessage(ErrorMessages.ZipCodeInvalid);

    }
}
