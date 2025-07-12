using Moq;
using PontoEstagio.Domain.Security.Token;

namespace CommonTestUltilities.Security
{
    public class TokenRefreshTokenMockBuilder
    {
        private readonly Mock<ITokenRefreshToken> _mock;

        public TokenRefreshTokenMockBuilder()
        {
            _mock = new Mock<ITokenRefreshToken>();
        }

        public TokenRefreshTokenMockBuilder SetupGenerateRefreshToken(string token)
        {
            _mock.Setup(x => x.GenerateRefreshToken())
                .Returns(token);
            return this;
        }

        public ITokenRefreshToken Build()
        {
            return _mock.Object;
        }
    }
}