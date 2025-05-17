using CommonTestUltilities.Entities;
using FluentAssertions;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class UserProjectTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var attendance = UserProjectBuilder.Build();
        // Assert
        attendance.Should().NotBeNull();
    }

    [Fact]
    public void CreateUserProject_WithEmptyUserId_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => UserProjectBuilder.Build(userId: Guid.Empty);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.InvalidUserId); 
    }

    [Fact]
    public void CreateUserProject_WithEmptyProjectId_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => UserProjectBuilder.Build(projectId: Guid.Empty);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.InvalidProjectId); 
    }

    [Fact]
    public void CreateUserProject_WithInvalidRole_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => UserProjectBuilder.Build(role: (UserType)999); 

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.InvalidUserType); 
    }
}