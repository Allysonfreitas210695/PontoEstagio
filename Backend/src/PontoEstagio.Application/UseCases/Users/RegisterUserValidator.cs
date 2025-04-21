using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.NameCannotBeEmpty);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EmailCannotBeEmpty)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ErrorMessages.InvalidEmailFormat);

        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}