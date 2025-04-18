namespace PontoEstagio.Domain.Entities;

public class UserRefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }

    public User User { get; set; } = default!;
}