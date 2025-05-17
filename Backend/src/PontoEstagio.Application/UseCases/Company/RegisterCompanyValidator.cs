using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Company;

public class RegisterCompanyValidator : AbstractValidator<RequestRegisterCompanytJson>
{
    public RegisterCompanyValidator()
    {
        RuleFor(company => company.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.Company_Name_Required)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.Company_Name_MinLength);

        RuleFor(company => company.CNPJ)
            .NotEmpty()
            .WithMessage(ErrorMessages.Company_CNPJ_Required)
            .Matches(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$")
            .WithMessage(ErrorMessages.Company_CNPJ_InvalidFormat);

        RuleFor(company => company.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.Company_Email_Required)
            .EmailAddress()
            .WithMessage(ErrorMessages.Company_Email_Invalid);

        RuleFor(company => company.Phone)
            .NotEmpty()
            .WithMessage(ErrorMessages.Company_Phone_Required)
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
            .WithMessage(ErrorMessages.Company_Phone_InvalidFormat);

        RuleFor(u => u.Address)
            .NotNull().WithMessage(ErrorMessages.AddressRequired)
            .SetValidator(new AddressValidatorCompany());
    }
}