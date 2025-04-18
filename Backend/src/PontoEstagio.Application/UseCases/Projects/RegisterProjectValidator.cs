using FluentValidation;
using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Projects;

public class RegisterProjectValidator : AbstractValidator<RequestRegisterProjectJson>
{
    public RegisterProjectValidator()
    {
        RuleFor(project => project.Name)
            .NotEmpty()
            .WithMessage("Project name is required.");

        RuleFor(project => project.StartDate)
            .Must(date => date.Date >= DateTime.UtcNow.Date)
            .WithMessage("Start date must be today or a future date.");

        RuleFor(project => project.EndDate)
            .Must((project, endDate) => endDate == null || endDate.Value.Date > project.StartDate.Date)
            .WithMessage("End date must be later than start date.")
            .When(project => project.EndDate.HasValue);
    }
}
