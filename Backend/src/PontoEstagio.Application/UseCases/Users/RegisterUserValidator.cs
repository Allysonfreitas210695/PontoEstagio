using FluentValidation;
using PontoEstagio.Communication.Request;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage("Email format is invalid.");

        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}