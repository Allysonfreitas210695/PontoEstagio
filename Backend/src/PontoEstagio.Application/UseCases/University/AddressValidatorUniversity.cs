using FluentValidation;
using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.University;

public class AddressValidatorUniversity : AbstractValidator<RequestAddressJson>
{
    public AddressValidatorUniversity()
    {
        RuleFor(a => a.Street)
            .NotEmpty().WithMessage("A rua é obrigatória.");

        RuleFor(a => a.Number)
            .NotEmpty().WithMessage("O número é obrigatório.");

        RuleFor(a => a.City)
            .NotEmpty().WithMessage("A cidade é obrigatória.");

        RuleFor(a => a.State)
            .NotEmpty().WithMessage("O estado é obrigatório.")
            .Length(2).WithMessage("O estado deve conter 2 letras (UF).");

        RuleFor(a => a.ZipCode)
            .NotEmpty().WithMessage("O CEP é obrigatório.")
            .Matches(@"^\d{8}$").WithMessage("O CEP deve conter exatamente 8 dígitos.");
    }
}

