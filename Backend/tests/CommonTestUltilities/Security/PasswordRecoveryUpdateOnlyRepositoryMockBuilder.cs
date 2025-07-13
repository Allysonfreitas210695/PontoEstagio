using Moq;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.PasswordRecovery;

namespace CommonTestUltilities.Security
{
    public class PasswordRecoveryUpdateOnlyRepositoryMockBuilder
    {
        private readonly Mock<IPasswordRecoveryUpdateOnlyRespository> _mock;

        public PasswordRecoveryUpdateOnlyRepositoryMockBuilder()
        {
            _mock = new Mock<IPasswordRecoveryUpdateOnlyRespository>();
        }

        public PasswordRecoveryUpdateOnlyRepositoryMockBuilder SetupGetByCode(PasswordRecovery recovery)
        {
            _mock.Setup(x => x.GetPasswordRecoveryByCode(recovery.Code))
                .ReturnsAsync(recovery);
            return this;
        }

        public PasswordRecoveryUpdateOnlyRepositoryMockBuilder SetupUpdate()
        {
            _mock.Setup(x => x.Update(It.IsAny<PasswordRecovery>()));
            return this;
        }

        public IPasswordRecoveryUpdateOnlyRespository Build() => _mock.Object;
        public Mock<IPasswordRecoveryUpdateOnlyRespository> GetMock() => _mock;
    }
}