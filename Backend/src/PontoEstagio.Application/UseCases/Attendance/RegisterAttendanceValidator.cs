using FluentValidation;
using PontoEstagio.Communication.Request;

namespace PontoEstagio.Application.UseCases.Attendance;

public class RegisterAttendanceValidator : AbstractValidator<RequestRegisterAttendanceJson>
    {
        public RegisterAttendanceValidator()
        {
             RuleFor(x => x.Date)
                .NotEmpty().WithMessage("The date is required.")
                .LessThanOrEqualTo(DateTime.Now.Date).WithMessage("The date cannot be in the future.");

            RuleFor(x => x.CheckIn)
                .NotEmpty().WithMessage("Check-in time is required.")
                .GreaterThan(TimeSpan.Zero).WithMessage("Check-in time must be a valid time.");

            RuleFor(x => x.CheckOut)
                .NotEmpty().WithMessage("Check-out time is required.")
                .GreaterThan(TimeSpan.Zero).WithMessage("Check-out time must be a valid time.")
                .GreaterThan(x => x.CheckIn).WithMessage("Check-out time must be later than check-in time.");
        }
    }