using PontoEstagio.Domain.Enum;

namespace PontoEstagio.Domain.Enum.Extensions;
public static class UserTypeExtensions
{
    public static string ToRoleName(this UserType userType)
    {
        return userType switch
        {
            UserType.Intern => "Intern",
            UserType.Supervisor => "Supervisor",
            UserType.Admin => "Admin",
            _ => throw new ArgumentOutOfRangeException(nameof(userType), userType, null)
        };
    }
}