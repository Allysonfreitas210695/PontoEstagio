using FluentValidation;
using PontoEstagio.Communication.Requests;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Application.UseCases.Activity;
public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(x => x.AttendanceId)
            .NotEmpty().WithMessage(ErrorMessages.AttendanceIdIsRequired);

        RuleFor(x => x.ProofFilePath)
            .Must(BeAValidBase64).WithMessage(ErrorMessages.InvalidBase64)
            .Must(BeLessThan5MB).WithMessage(ErrorMessages.FileTooLarge)
            .When(x => !string.IsNullOrWhiteSpace(x.ProofFilePath));
    }

    private bool BeAValidBase64(string base64)
    {
        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool BeLessThan5MB(string base64)
    {
        try
        {
            var bytes = Convert.FromBase64String(base64);
            const int maxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
            return bytes.Length <= maxFileSizeInBytes;
        }
        catch
        {
            return false; 
        }
    }

}
