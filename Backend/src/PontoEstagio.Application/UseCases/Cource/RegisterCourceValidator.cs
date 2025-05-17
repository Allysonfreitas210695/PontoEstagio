using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Cource;

public class RegisterCourceValidator : AbstractValidator<RequestRegisterCourceJson>
{
    public RegisterCourceValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.CourseNameRequired)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.CourseNameMinLength);

        RuleFor(c => c.WorkloadHours)
            .NotEmpty()
            .WithMessage(ErrorMessages.WorkloadHoursRequired)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.WorkloadHoursGreaterThanZero);

        RuleFor(c => c.UniversityId)
            .NotEmpty()
            .WithMessage(ErrorMessages.UniversityIdRequired);
    }
}
