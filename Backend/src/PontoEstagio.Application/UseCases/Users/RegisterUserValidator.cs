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
            .WithMessage(ErrorMessages.InvalidUserName);  

        RuleFor(user => user.UniversityId)
            .NotEmpty()
            .WithMessage(ErrorMessages.InvalidUniversityId)
            .Must(id => id != Guid.Empty)
            .WithMessage(ErrorMessages.InvalidUniversityId);

        RuleFor(user => user.CourseId)
            .Must((user, courseId) =>
            {
                if (user.Type == PontoEstagio.Communication.Enum.UserType.Coordinator)
                {
                    return courseId != null && courseId != Guid.Empty;
                }
                return true;
            })
            .WithMessage(ErrorMessages.InvalidCourseIdForUserType);

        RuleFor(user => user.Registration)
            .NotEmpty()
            .WithMessage(ErrorMessages.InvalidRegistration)
            .When(user => user.Type == PontoEstagio.Communication.Enum.UserType.Intern); 

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EmailCannotBeEmpty)
            .EmailAddress()
            .WithMessage(ErrorMessages.InvalidEmailFormat);

        RuleFor(user => user.Phone)
            .NotEmpty()
            .WithMessage(ErrorMessages.PhoneIsRequired)
            .MaximumLength(20)
            .WithMessage(ErrorMessages.PhoneMaxLength)
            .When(user => user.Type == PontoEstagio.Communication.Enum.UserType.Intern);

        RuleFor(user => user.CPF)
            .NotEmpty()
            .WithMessage(ErrorMessages.InvalidCpf) 
            .Matches(@"^\d{11}$")
            .WithMessage(ErrorMessages.UserInvalidCpfFormat)
            .When(user => user.Type == PontoEstagio.Communication.Enum.UserType.Advisor);

        RuleFor(user => user.Department)
            .NotEmpty()
            .WithMessage(ErrorMessages.InvalidDepartment)
            .When(user => user.Type == PontoEstagio.Communication.Enum.UserType.Advisor);

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.InvalidPassword)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}