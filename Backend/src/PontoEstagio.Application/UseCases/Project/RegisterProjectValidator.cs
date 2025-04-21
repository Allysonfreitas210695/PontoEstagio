using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Projects;

public class RegisterProjectValidator : AbstractValidator<RequestRegisterProjectJson>
{
    public RegisterProjectValidator()
    {
        RuleFor(project => project.Name)
           .NotEmpty()
           .WithMessage(ErrorMessages.ProjectNameRequired);

        RuleFor(project => project.StartDate)
            .Must(date => date.Date >= DateTime.UtcNow.Date)
            .WithMessage(ErrorMessages.StartDateMustBeTodayOrFuture);

        RuleFor(project => project.EndDate)
            .Must((project, endDate) => endDate == null || endDate.Value.Date > project.StartDate.Date)
            .WithMessage(ErrorMessages.EndDateMustBeLaterThanStartDate)
            .When(project => project.EndDate.HasValue);
    }
}
