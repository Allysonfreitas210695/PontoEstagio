using Moq;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.Repositories;

namespace CommonTestUltilities.Repositories;

public class UserReadOnlyRepositoryMockBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _mock;

    public UserReadOnlyRepositoryMockBuilder()
    {
        _mock = new Mock<IUserReadOnlyRepository>();
    }

    public UserReadOnlyRepositoryMockBuilder GetUserByEmailAsync(User user)
    {
        _mock.Setup(x => x.GetUserByEmailAsync(user.Email.Endereco))
            .ReturnsAsync(user);
        return this;
    }

    public UserReadOnlyRepositoryMockBuilder GetUserByEmailAsync_NotFound(string email)
    {
        _mock.Setup(x => x.GetUserByEmailAsync(email))
            .ReturnsAsync((User)null);
        return this;
    }

    public IUserReadOnlyRepository Build()
    {
        return _mock.Object;
    }

    public Mock<IUserReadOnlyRepository> GetMock()
    {
        return _mock;
    }
}