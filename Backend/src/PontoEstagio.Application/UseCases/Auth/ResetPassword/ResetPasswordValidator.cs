
using FluentValidation;
using PontoEstagio.Application.Helpers;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Auth.ResetPassword;
public class ResetPasswordValidator : AbstractValidator<RequestResetPasswordJson>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage(ErrorMessages.ResetPassword_Code_Required)
            .Length(6).WithMessage(ErrorMessages.ResetPassword_Code_Length);

        RuleFor(user => user.NewPassword)
            .SetValidator(new PasswordValidator<RequestResetPasswordJson>());
    }
}
