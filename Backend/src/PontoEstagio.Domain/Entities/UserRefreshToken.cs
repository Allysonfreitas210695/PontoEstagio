using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace PontoEstagio.Domain.Entities;

public class UserRefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }

    public User User { get; set; } = default!;

    public UserRefreshToken() { }
    public UserRefreshToken(Guid? id, Guid userId, string token, DateTime expirationDate)
    {
        Id = id ?? Guid.NewGuid();

        if (userId == Guid.Empty)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidUserId });

         if (string.IsNullOrWhiteSpace(token))
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.InvalidToken });

        if (expirationDate.Date <= DateTime.Now.Date)
            throw new ErrorOnValidationException(new List<string> { ErrorMessages.invalidExpirationDate });

        UserId = userId;
        Token = token;
        ExpirationDate = expirationDate;
    }
}