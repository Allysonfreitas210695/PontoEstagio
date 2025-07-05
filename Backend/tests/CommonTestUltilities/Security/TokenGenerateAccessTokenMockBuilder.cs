using Moq;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Security.Token;

namespace CommonTestUltilities.Security
{
    public class TokenGenerateAccessTokenMockBuilder
    {
        private readonly Mock<ITokenGenerateAccessToken> _mock;

        public TokenGenerateAccessTokenMockBuilder()
        {
            _mock = new Mock<ITokenGenerateAccessToken>();
        }

        public TokenGenerateAccessTokenMockBuilder SetupGenerateAccessToken(User user, string token)
        {
            _mock.Setup(x => x.GenerateAccessToken(user))
                .Returns(token);
            return this;
        }

        public ITokenGenerateAccessToken Build()
        {
            return _mock.Object;
        }
    }
}