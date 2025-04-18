using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";

    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Password cannot be empty.");
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Password must be at least 8 characters long.");
            return false;
        }

        if (!Regex.IsMatch(password, "[A-Z]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Password must contain at least one uppercase letter.");
            return false;
        }

        if (!Regex.IsMatch(password, "[a-z]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Password must contain at least one lowercase letter.");
            return false;
        }

        if (!Regex.IsMatch(password, "[0-9]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Password must contain at least one number.");
            return false;
        }

        if (!Regex.IsMatch(password, @"[\W_]"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Password must contain at least one special character.");
            return false;
        }

        return true;
    }
}
