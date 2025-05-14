using FluentValidation;
using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.University;

public class RegisterUniversityValidator : AbstractValidator<RequestRegisterUniversityJson>
{
    public RegisterUniversityValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("O nome da universidade é obrigatório.")
            .MaximumLength(150).WithMessage("O nome da universidade deve ter no máximo 150 caracteres.");

        RuleFor(u => u.Acronym)
            .NotEmpty().WithMessage("A sigla da universidade é obrigatória.")
            .MaximumLength(10).WithMessage("A sigla deve ter no máximo 10 caracteres.");

        RuleFor(u => u.CNPJ)
            .NotEmpty().WithMessage("O CNPJ é obrigatório.")
            .Length(14).WithMessage("O CNPJ deve conter exatamente 14 dígitos numéricos.")
            .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter apenas números.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");

        RuleFor(u => u.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter entre 10 e 11 dígitos numéricos.");

        RuleFor(u => u.Address)
            .NotNull().WithMessage("O endereço é obrigatório.")
            .SetValidator(new AddressValidatorUniversity());
    }
}
