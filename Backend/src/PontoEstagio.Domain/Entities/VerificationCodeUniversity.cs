using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Domain.ValueObjects;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;
public class VerificationCodeUniversity : Entity
{
    public Email Email { get; private set; } = default!;
    public string Code { get; private set; } = null!;
    public DateTime Expiration { get; private set; }
    public VerificationCodeStatus Status { get; private set; }

    protected VerificationCodeUniversity() { }

    public VerificationCodeUniversity(Guid? id, Email email, string code, DateTime expiration)
    {
        Id = id ?? Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(code))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidVerificationCode });

        if (expiration <= DateTime.UtcNow)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidExpirationDate });

        Email = email;
        Code = code;
        CreatedAt = DateTime.UtcNow;
        Expiration = expiration;
        Status = VerificationCodeStatus.Active;
    }

    public void MarkAsUsed()
    {
        if (Status != VerificationCodeStatus.Active)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.VerificationCodeNotActive });

        Status = VerificationCodeStatus.Used;
        UpdateTimestamp();
    }

    public void MarkAsExpired()
    {
        Status = VerificationCodeStatus.Expired;
        UpdateTimestamp();
    }
}