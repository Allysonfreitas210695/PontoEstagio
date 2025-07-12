using Moq;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.User;
using System.Threading.Tasks;

namespace CommonTestUltilities.Repositories
{
    public class UserRefreshTokenRepositoryMockBuilder
    {
        private readonly Mock<IUserRefreshTokenRepository> _mock;

        public UserRefreshTokenRepositoryMockBuilder()
        {
            _mock = new Mock<IUserRefreshTokenRepository>();
        }

        public UserRefreshTokenRepositoryMockBuilder SetupInsertAsync()
        {
            _mock.Setup(x => x.InsertAsync(It.IsAny<UserRefreshToken>()))
                .Returns(Task.CompletedTask);
            return this;
        }

        public IUserRefreshTokenRepository Build()
        {
            return _mock.Object;
        }

        public Mock<IUserRefreshTokenRepository> GetMock()
        {
            return _mock;
        }
    }
}