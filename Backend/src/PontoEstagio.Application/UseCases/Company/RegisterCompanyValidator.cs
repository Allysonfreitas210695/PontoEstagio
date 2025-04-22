using FluentValidation;
using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Company;

public class RegisterCompanyValidator : AbstractValidator<RequestRegisterCompanytJson>
{
    public RegisterCompanyValidator()
    {
        RuleFor(company => company.Name)
            .NotEmpty()
            .WithMessage("Company name is required.")
            .MinimumLength(3)
            .WithMessage("Company name must be at least 3 characters long.");

        RuleFor(company => company.CNPJ)
            .NotEmpty()
            .WithMessage("CNPJ is required.")
            .Matches(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$")
            .WithMessage("CNPJ must be in the format XX.XXX.XXX/XXXX-XX.");

        RuleFor(company => company.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(company => company.Phone)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
            .WithMessage("Phone number must be in the format (XX) XXXX-XXXX or (XX) XXXXX-XXXX.");
    }
}