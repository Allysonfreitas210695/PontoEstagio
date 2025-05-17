using System;
using PontoEstagio.Domain.Common;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class PasswordRecovery : Entity
{
    private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(3);
    public Guid UserId { get; private set; }
    public string Code { get; private set; } = default!; 
    public bool Used { get; private set; } = false;
    public User User { get; set; } = default!;
    
    public PasswordRecovery() { }

    public PasswordRecovery(Guid? id, Guid userId, string code)
    {
        if (userId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUserId });

        if (string.IsNullOrWhiteSpace(code))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.CodeRequired });


        Id = id ?? Guid.NewGuid();
        UserId = userId;
        Code = code;
        CreatedAt = DateTime.UtcNow; 
        Used = false;
    }

    public void MarkAsUsed()
    {
        if (Used)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.AlreadyUsed });

        Used = true;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > CreatedAt.Add(ExpirationTime);
    }

    public void EnsureUsable()
    {
        if (Used)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.AlreadyUsed });

        if (IsExpired())
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.Expired });
    }
}
