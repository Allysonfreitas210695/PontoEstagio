using FluentValidation;
using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Login.DoLogin;

public class LoginValidator : AbstractValidator<RequestLoginUserJson>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage("Email format is invalid.");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.");
    }
}