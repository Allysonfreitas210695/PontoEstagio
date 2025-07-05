using FluentValidation;
using PontoEstagio.Application.Helpers;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.NameCannotBeEmpty);

        RuleFor(user => user.UniversityId)
            .NotEmpty()
            .WithMessage(ErrorMessages.InvalidUniversityId)
            .Must(id => id != Guid.Empty)
            .WithMessage(ErrorMessages.InvalidUniversityId);

        RuleFor(user => user.CourseId)
            .Must((user, courseId) =>
            {
                if (user.Type == PontoEstagio.Communication.Enum.UserType.Intern || user.Type == PontoEstagio.Communication.Enum.UserType.Coordinator)
                {
                    return courseId != null && courseId != Guid.Empty;
                }
                return true;
            })
            .WithMessage(ErrorMessages.InvalidCourseIdForUserType);

        RuleFor(user => user.Registration)
            .NotEmpty()
            .WithMessage(ErrorMessages.InvalidRegistration);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EmailCannotBeEmpty)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ErrorMessages.InvalidEmailFormat);
        
        RuleFor(user => user.Phone)
            .NotEmpty()
            .WithMessage(ErrorMessages.PhoneIsRequired)
            .MaximumLength(20)
            .WithMessage(ErrorMessages.PhoneMaxLength);

        RuleFor(user => user.CPF)
            .NotEmpty()
            .WithMessage(ErrorMessages.CpfIsRequired)
            .Matches(@"^\d{11}$")
            .WithMessage(ErrorMessages.UserInvalidCpfFormat)
            .When(user => user.Type == PontoEstagio.Communication.Enum.UserType.Advisor);

        RuleFor(user => user.Department)
            .NotEmpty()
            .WithMessage(ErrorMessages.DepartmentIsRequired)
            .When(user => user.Type == PontoEstagio.Communication.Enum.UserType.Advisor);


        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}