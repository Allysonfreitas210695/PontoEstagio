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
            .WithMessage(ErrorMessages.ProjectNameRequired)
            .MinimumLength(3)
            .WithMessage(ErrorMessages.InvalidProjectNameLength);

        RuleFor(project => project.CompanyId)
            .NotEqual(Guid.Empty)
            .WithMessage(ErrorMessages.InvalidCompanyId);

        RuleFor(project => project.StartDate)
            .Must(date => date.Date >= DateTime.UtcNow.Date)
            .WithMessage(ErrorMessages.StartDateMustBeTodayOrFuture);

        RuleFor(project => project.TotalHours)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.InvalidTotalHours);

        RuleFor(project => project.EndDate)
            .Must((project, endDate) => endDate == null || endDate.Value.Date > project.StartDate.Date)
            .WithMessage(ErrorMessages.EndDateMustBeLaterThanStartDate)
            .When(project => project.EndDate.HasValue);
    }
}
