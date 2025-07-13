using Moq;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories.User;

namespace CommonTestUltilities.Repositories;

public class UserUpdateOnlyRepositoryMockBuilder
{
    private readonly Mock<IUserUpdateOnlyRepository> _mock;

    public UserUpdateOnlyRepositoryMockBuilder()
    {
        _mock = new Mock<IUserUpdateOnlyRepository>();
    }

    public UserUpdateOnlyRepositoryMockBuilder SetupGetUserByIdAsync(User user)
    {
        _mock.Setup(x => x.GetUserByIdAsync(user.Id))
            .ReturnsAsync(user);
        return this;
    }

    public UserUpdateOnlyRepositoryMockBuilder SetupUpdate()
    {
        _mock.Setup(x => x.Update(It.IsAny<User>()));
        return this;
    }

    public IUserUpdateOnlyRepository Build() => _mock.Object;
    public Mock<IUserUpdateOnlyRepository> GetMock() => _mock;
}
