using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Company;

public class RegisterLegalRepresentativeValidator : AbstractValidator<RequestRegisterLegalRepresentativeJson>
{
    public RegisterLegalRepresentativeValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage(ErrorMessages.RepresentativeNameRequired);

        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage(ErrorMessages.RepresentativeCpfRequired)
            .Matches(@"^\d{11}$").WithMessage(ErrorMessages.RepresentativeCpfInvalid);

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage(ErrorMessages.RepresentativePositionRequired)
            .MaximumLength(100).WithMessage(ErrorMessages.RepresentativePositionTooLong);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ErrorMessages.RepresentativeEmailRequired)
            .EmailAddress().WithMessage(ErrorMessages.RepresentativeEmailInvalid);
    }
}
