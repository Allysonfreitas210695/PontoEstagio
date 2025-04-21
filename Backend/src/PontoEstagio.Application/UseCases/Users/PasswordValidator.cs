using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;
using PontoEstagio.Exceptions.ResourcesErrors;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";

    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ErrorMessages.PasswordEmpty);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ErrorMessages.PasswordTooShort);
            return false;
        }

        if (!Regex.IsMatch(password, "[A-Z]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ErrorMessages.PasswordMissingUppercase);
            return false;
        }

        if (!Regex.IsMatch(password, "[a-z]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ErrorMessages.PasswordMissingLowercase);
            return false;
        }

        if (!Regex.IsMatch(password, "[0-9]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ErrorMessages.PasswordMissingNumber);
            return false;
        }

        if (!Regex.IsMatch(password, @"[\W_]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ErrorMessages.PasswordMissingSpecialCharacter);
            return false;
        }

        return true;
    }
}
