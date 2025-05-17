using CommonTestUltilities.Entities;
using FluentAssertions;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class UserRefreshTokenTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var attendance = UserRefreshTokenBuilder.Build();
        // Assert
        attendance.Should().NotBeNull();
    }

    [Fact]
    public void CreateUserRefreshToken_WithEmptyToken_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => UserRefreshTokenBuilder.Build(token: "");

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .And.Errors.Should().Contain(ErrorMessages.InvalidToken);
    }

    [Fact]
    public void CreateUserRefreshToken_WithNullToken_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => UserRefreshTokenBuilder.Build(expirationDate: DateTime.Now.AddDays(-1));

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .And.Errors.Should().Contain(ErrorMessages.invalidExpirationDate);
    }

}