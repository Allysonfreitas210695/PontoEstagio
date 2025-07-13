using Moq;
using PontoEstagio.Domain.Security.Cryptography;

namespace CommonTestUltilities.Security
{
    public class PasswordEncrypterMockBuilder
    {
        private readonly Mock<IPasswordEncrypter> _mock;

        public PasswordEncrypterMockBuilder()
        {
            _mock = new Mock<IPasswordEncrypter>();
        }

        public PasswordEncrypterMockBuilder Verify(string password, string hash, bool isValid)
        {
            _mock.Setup(x => x.Verify(password, hash))
                .Returns(isValid);
            return this;
        }

        public PasswordEncrypterMockBuilder Encrypt(string plainPassword, string encrypted)
        {
            _mock.Setup(x => x.Encrypt(plainPassword))
                .Returns(encrypted);
            return this;
        }


        public IPasswordEncrypter Build()
        {
            return _mock.Object;
        }
    }
}