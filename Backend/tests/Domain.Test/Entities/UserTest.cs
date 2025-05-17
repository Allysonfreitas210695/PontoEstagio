using CommonTestUltilities.Entities;
using FluentAssertions;
using PontoEstagio.Domain.Enum;

namespace Domain.Test.Entities;

public class UserTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var project = UserBuilder.Build();

        // Assert
        project.Should().NotBeNull();
        project.Name.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Activate_ShouldSetIsActiveTrue()
    {
        // Arrange
        var user = UserBuilder.Build(isActive: false);
        var lastUpdate = user.UpdatedAt;

        // Act
        user.Activate();

        // Assert
        user.IsActive.Should().BeTrue();
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void Inactivate_ShouldSetIsActiveFalse()
    {
        // Arrange
        var user = UserBuilder.Build(isActive: true);
        var lastUpdate = user.UpdatedAt;

        // Act
        user.Inactivate();

        // Assert
        user.IsActive.Should().BeFalse();
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateType_ShouldUpdateValue()
    {
        // Arrange
        var user = UserBuilder.Build(type: UserType.Intern);
        var newType = UserType.Supervisor;
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateType(newType);

        // Assert
        user.Type.Should().Be(newType);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateName_ShouldUpdateValue()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newName = "Novo Nome";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateName(newName);

        // Assert
        user.Name.Should().Be(newName);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateEmail_ShouldChangeEmail()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newEmail = "teste@email.com";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateEmail(newEmail);

        // Assert
        user.Email.Endereco.Should().Be(newEmail);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdatePassword_ShouldChangePassword()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newPassword = "novaSenha123";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdatePassword(newPassword);

        // Assert
        user.Password.Should().Be(newPassword);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }
}