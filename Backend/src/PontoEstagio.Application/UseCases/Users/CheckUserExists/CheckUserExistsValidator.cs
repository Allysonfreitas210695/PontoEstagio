using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Users.CheckUserExists;

public class CheckUserExistsValidator : AbstractValidator<RequestCheckUserExistsUserJson>
{
    public CheckUserExistsValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EmailCannotBeEmpty)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ErrorMessages.InvalidEmailFormat);
            
        RuleFor(user => user.Password)
            .SetValidator(new PasswordCheckUserValidator<RequestCheckUserExistsUserJson>());
    }
}
