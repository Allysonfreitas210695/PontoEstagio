using Moq;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.PasswordRecovery;

namespace CommonTestUltilities.Security;

public class PasswordRecoveryWriteOnlyRepositoryMockBuilder
{
    private readonly Mock<IPasswordRecoveryWriteOnlyRespository> _mock;

public PasswordRecoveryWriteOnlyRepositoryMockBuilder()
{
    _mock = new Mock<IPasswordRecoveryWriteOnlyRespository>();
}

public PasswordRecoveryWriteOnlyRepositoryMockBuilder SetupAddAsync()
{
    _mock.Setup(x => x.AddAsync(It.IsAny<PasswordRecovery>()))
        .Returns(Task.CompletedTask);
    return this;
}

public IPasswordRecoveryWriteOnlyRespository Build() => _mock.Object;
public Mock<IPasswordRecoveryWriteOnlyRespository> GetMock() => _mock;
}