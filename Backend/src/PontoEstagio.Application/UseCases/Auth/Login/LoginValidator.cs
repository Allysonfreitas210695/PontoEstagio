using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Login.DoLogin;

public class LoginValidator : AbstractValidator<RequestLoginUserJson>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EmailCannotBeEmpty)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ErrorMessages.InvalidEmailFormat);

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.PasswordCannotBeEmpty);
    }
}