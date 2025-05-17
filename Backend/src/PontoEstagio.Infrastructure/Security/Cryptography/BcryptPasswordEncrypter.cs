using PontoEstagio.Domain.Security.Cryptography;

namespace PontoEstagio.Infrastructure.Security;

public class BcryptPasswordEncrypter : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
