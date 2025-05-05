using FluentValidation;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Attendance;

public class RegisterAttendanceValidator : AbstractValidator<RequestRegisterAttendanceJson>
    {
    public RegisterAttendanceValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage(ErrorMessages.DateIsRequired)
            .LessThanOrEqualTo(DateTime.Now.Date).WithMessage(ErrorMessages.DateCannotBeInTheFuture);

        RuleFor(x => x.CheckIn)
            .NotEmpty().WithMessage(ErrorMessages.CheckInTimeIsRequired)
            .GreaterThan(TimeSpan.Zero).WithMessage(ErrorMessages.CheckInTimeMustBeValid);

        RuleFor(x => x.CheckOut)
            .NotEmpty().WithMessage(ErrorMessages.CheckOutTimeIsRequired)
            .GreaterThan(TimeSpan.Zero).WithMessage(ErrorMessages.CheckOutTimeMustBeValid)
            .GreaterThan(x => x.CheckIn).WithMessage(ErrorMessages.CheckOutMustBeLaterThanCheckIn);

        RuleFor(x => x.ProofImageBase64)
           .NotEmpty().WithMessage(ErrorMessages.InvalidProofImage);
    }
}